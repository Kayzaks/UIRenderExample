using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UIRenderExample
{
    public struct SUIStyle
    {
        public enum STYLE_ORIENTATION : int
        {
            VERTICAL = 0,
            HORIZONTAL = 1
        }

        public Color Background;
        public STYLE_ORIENTATION Orientation;
    }

    public struct SUIMargin
    {
        public double Left;
        public double Top;
        public double Right;
        public double Bottom;

        public SUIMargin(double inLeft, double inTop, double inRight, double inBottom)
        {
            Left = inLeft;
            Top = inTop;
            Right = inRight;
            Bottom = inBottom;
        }        

        public double TotalWidth
        {
            get
            {
                return Left + Right;
            }
        }

        public double TotalHeight
        {
            get
            {
                return Top + Bottom;
            }
        }
    }

    public struct SUIMeasure
    {
        public double Width;
        public double Height;

        public SUIMeasure(double inWidth, double inHeight)
        {
            Width = inWidth;
            Height = inHeight;
        }
    }
}
