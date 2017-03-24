using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIRenderExample
{
    public class SUILabel : SUIElement
    {
        public string LabelText;

        public SUILabel(object inContent)
            : base(inContent)
        {
            LabelText = inContent as string;

            if (LabelText == null)
            {
                LabelText = "empty";
            }
        }

        public override void draw(Canvas inCanvas, Point inOffset)
        {
            Point currentOffset = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);
            // This is obviously a bit of cheating here,
            // but any Text rendering routine will do
            TextBlock lblText = new TextBlock();
            lblText.Text = LabelText;

            Canvas.SetLeft(lblText, currentOffset.X);
            Canvas.SetTop(lblText, currentOffset.Y);

            inCanvas.Children.Add(lblText);
        }

        public override void measure(Point inOffset)
        {
            base.measure(inOffset);

            // How you measure your text is up to you really.
            // If you are rendering each character as a single
            // sprite, obviously the text will be the size
            // of all those sprites combined.

            // Here we use a method from StackOverflow (https://stackoverflow.com/questions/9264398/how-to-calculate-wpf-textblock-width-for-its-known-font-size-and-characters)
            // for our TextBlock-Cheat

            TextBlock lblText = new TextBlock(); // Declared as in the draw method to find out it's font, etc.

            var formattedText = new FormattedText(
                LabelText,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(lblText.FontFamily, lblText.FontStyle, lblText.FontWeight, lblText.FontStretch),
                lblText.FontSize,
                Brushes.Black);

            Measure = new SUIMeasure(formattedText.Width, formattedText.Height);
        }
    }
}
