using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace UIRenderExample
{
    public class SUIElement
    {
        protected List<SUIElement> Children;
        protected SUIMargin Margin;
        protected SUIMeasure Measure;
        protected SUIStyle Style;
        protected Point AbsolutePosition;

        public static readonly Point ZERO_OFFSET = new Point(0, 0);

        public SUIElement(object inContent)
        {
            Children = new List<SUIElement>();
            Margin = new SUIMargin(0, 0, 0, 0);
            Measure = new SUIMeasure(0, 0);
        }

        protected virtual void onMouse(Point inPosition, bool isPressed)
        {
            // Standard case, we have no need for the mouse
        }

        public virtual void draw(Canvas inCanvas, Point inOffset)
        {
            // First, we measure everything
            measure(inOffset);

            // We add the margin
            Point currentOffset = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);

            if (Style.Background != null)
            {
                // Let's draw some global "style" elements - like the background
                Rectangle styleRect = new Rectangle();
                styleRect.Fill = new SolidColorBrush(Style.Background);
                styleRect.Width = Measure.Width - Margin.TotalWidth;
                styleRect.Height = Measure.Height - Margin.TotalHeight;
                Canvas.SetLeft(styleRect, currentOffset.X);
                Canvas.SetTop(styleRect, currentOffset.Y);
                inCanvas.Children.Add(styleRect);
            }
        }

        public virtual void measure(Point inOffset)
        {
            AbsolutePosition = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);
            Measure = new SUIMeasure(Margin.TotalWidth, Margin.TotalHeight);
        }

        public void handleMouseInput(Point inPosition, bool isPressed)
        {
            // We let this element handle a mouse press
            onMouse(inPosition, isPressed);

            // But we must also remember the Children!
            foreach(var child in Children)
            {
                child.handleMouseInput(inPosition, isPressed);
            }
        }

        public void setStyle(SUIStyle inStyle)
        {
            Style = inStyle;
        }

        public void setMargin(double inLeft, double inTop, double inRight, double inBottom)
        {
            Margin = new SUIMargin(inLeft, inTop, inRight, inBottom);
        }

        public double getMeasuredHeight()
        {
            return Measure.Height;
        }

        public double getMeasuredWidth()
        {
            return Measure.Width;
        }


        public static SUIElement PARSE_XML(string inFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(inFileName);

            XmlNodeList topNode = xmlDoc.GetElementsByTagName("body");

            if (topNode.Count > 0)
            {
                return PARSE_XML_OBJECT(topNode[0]);
            }

            return null;
        }

        private static SUIElement PARSE_XML_OBJECT(XmlNode inNode)
        {
            SUIElement currentElement;

            switch(inNode.Name)
            {
                default:
                case "body":
                case "div":

                    currentElement = new SUIDivPanel(null);

                    break;

                case "button":

                    currentElement = new SUIButton(inNode.InnerText);

                    break;

                case "label":

                    currentElement = new SUILabel(inNode.InnerText);

                    break;
            }

            if (inNode.Attributes != null)
            {
                if (inNode.Attributes["style"] != null)
                {
                    currentElement.Style = PARSE_STYLE(inNode.Attributes["style"].Value);
                }
            }


            foreach (XmlNode node in inNode.ChildNodes)
            {
                SUIElement element = PARSE_XML_OBJECT(node);

                if (element != null)
                {
                    currentElement.Children.Add(element);
                }
            }

            return currentElement;
        }

        private static SUIStyle PARSE_STYLE(string inStyle)
        {
            SUIStyle outStyle = new SUIStyle();
            string[] stylings = inStyle.Split(';');

            foreach(string style in stylings)
            {
                if (style.Length > 0)
                {
                    if (style.ToLower().Contains("orientation:"))
                    {
                        // TODO: Parse correctly
                        outStyle.Orientation = SUIStyle.STYLE_ORIENTATION.HORIZONTAL;
                    }
                }
            }

            return outStyle;
        }
    }
}
