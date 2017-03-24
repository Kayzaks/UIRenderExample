using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UIRenderExample
{
    public class SUIButton : SUIElement
    {
        public Action Click;

        public enum BUTTON_STATE
        {
            MOUSE_UP,
            MOUSE_OVER,
            MOUSE_DOWN,
        }

        private static readonly double BUTTON_WIDTH = 100;
        private static readonly double BUTTON_HEIGHT = 40;
        private SUILabel Label;
        private BUTTON_STATE ButtonState = BUTTON_STATE.MOUSE_UP;
        private bool MousePressOnButton;

        public SUIButton(object inContent)
            : base(inContent)
        {
            // At this point we check what content
            // The button should have. We could also
            // choose to support an Image as a button
            // interior
            if (inContent is string)
            {
                Label = new SUILabel(inContent);
                Label.setMargin(30, 10, 0, 0);
                Children.Add(Label);
            }
        }

        public override void draw(Canvas inCanvas, Point inOffset)
        {
            base.draw(inCanvas, inOffset);
            
            // We add the margin
            Point currentOffset = new Point(inOffset.X + Margin.Left, inOffset.Y + Margin.Top);

            Rectangle btnRect = new Rectangle();
            // First we draw the exterior of the button
            switch (ButtonState)
            {
                default:
                case BUTTON_STATE.MOUSE_UP:
                    btnRect.Fill = new SolidColorBrush(Colors.LightGray);
                    // The standard case
                    break;

                case BUTTON_STATE.MOUSE_OVER:
                    btnRect.Fill = new SolidColorBrush(Colors.LightBlue);
                    // Mouse is hovering over the button
                    break;

                case BUTTON_STATE.MOUSE_DOWN:
                    btnRect.Fill = new SolidColorBrush(Colors.DarkBlue);
                    // Button was just pressed
                    break;
            }
            
            btnRect.Stroke = new SolidColorBrush(Colors.Black);
            btnRect.Width = Measure.Width - 6;
            btnRect.Height = Measure.Height - 6;

            Canvas.SetLeft(btnRect, 3 + currentOffset.X);
            Canvas.SetTop(btnRect, 3 + currentOffset.Y);
            inCanvas.Children.Add(btnRect);

            foreach(var child in Children)
            {
                child.draw(inCanvas, currentOffset);
            }
        }

        protected override void onMouse(Point inPosition, bool isPressed)
        {
            // We check if the mouse is inside our rectangle
            if (inPosition.X > AbsolutePosition.X && inPosition.Y > AbsolutePosition.Y
             && inPosition.X < AbsolutePosition.X + Measure.Width && inPosition.Y < AbsolutePosition.Y + Measure.Height)
            {
                if (isPressed == true)
                {
                    ButtonState = BUTTON_STATE.MOUSE_DOWN;
                    MousePressOnButton = true;
                }
                else
                {
                    ButtonState = BUTTON_STATE.MOUSE_OVER;

                    if (MousePressOnButton == true)
                    {
                        // The mouse was pressed over the button and 
                        // now released. This is the usual "click" 
                        // of a button
                        MousePressOnButton = false;

                        if (Click != null)
                        {
                            // Execute the Click event
                            Click();
                        }
                    }
                }
            }
            else
            {
                // Mouse is not over our Button
                ButtonState = BUTTON_STATE.MOUSE_UP;
                MousePressOnButton = false;
            }
        }

        public override void measure(Point inOffset)
        {
            base.measure(inOffset);

            // For the button, let's assume a fixed measure, ignoring
            // the content
            Measure = new SUIMeasure(BUTTON_WIDTH, BUTTON_HEIGHT);
        }
    }
}
