using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIRenderExample
{
    public class SUIDivPanel : SUIElement
    {
        public SUIDivPanel(object inContent)
            : base(inContent)
        {
            // Just a quick check if the content of this Div is a List of other
            // Elements
            if (inContent is IList)
            {
                foreach(object listItem in (inContent as IList))
                {
                    if (listItem is SUIElement)
                    {
                        Children.Add((SUIElement)listItem);
                    }
                }
            }
            // Or just a single Element
            else if (inContent is SUIElement)
            {
                Children.Add((SUIElement) inContent);
            }
        }



        public override void draw(Canvas inCanvas, Point inOffset)
        {
            base.draw(inCanvas, inOffset);

            // We add the margin
            Point currentOffset = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);

            // The Div will simply organize it's Children

            foreach (var child in Children)
            {
                child.draw(inCanvas, currentOffset);

                // We then move on to the next element, by offsetting
                // right under/next to the previous element depending on Orientation

                switch (Style.Orientation)
                {
                    default:
                    case SUIStyle.STYLE_ORIENTATION.VERTICAL:
                        currentOffset.Y = currentOffset.Y + child.getMeasuredHeight();
                        break;

                    case SUIStyle.STYLE_ORIENTATION.HORIZONTAL:
                        currentOffset.X = currentOffset.X + child.getMeasuredWidth();
                        break;
                }
            }
        }

        public override void measure(Point inOffset)
        {
            base.measure(inOffset);

            // As we did in draw(), we must make sure to calculate all
            // Children as if they were one below the other

            Measure = new SUIMeasure(Margin.TotalWidth, Margin.TotalWidth);

            Point currentOffset = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);

            foreach (var child in Children)
            {
                child.measure(currentOffset);

                switch (Style.Orientation)
                {
                    default:
                    case SUIStyle.STYLE_ORIENTATION.VERTICAL:
                        currentOffset.Y = currentOffset.Y + child.getMeasuredHeight();
                        Measure.Height = Measure.Height + child.getMeasuredHeight();
                        Measure.Width = Math.Max(Measure.Width, child.getMeasuredWidth() + Margin.TotalWidth);
                        break;

                    case SUIStyle.STYLE_ORIENTATION.HORIZONTAL:
                        currentOffset.X = currentOffset.X + child.getMeasuredWidth();
                        Measure.Width = Measure.Width + child.getMeasuredWidth();
                        Measure.Height = Math.Max(Measure.Height, child.getMeasuredHeight() + Margin.TotalHeight);
                        break;
                }
            }
        }
    }
}
