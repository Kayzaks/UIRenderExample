using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIRenderExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SUIElement rootElement;

        SUILabel test4;

        public MainWindow()
        {
            InitializeComponent();

            rootElement = SUIElement.PARSE_XML("../../../example.xml");

            rootElement.setStyle(new SUIStyle() { Background = Colors.LightGreen });
            rootElement.setMargin(50, 50, 0, 0);
            rootElement.draw(mCanvas, new Point(0, 0));
        }


        public void buttonClicked()
        {
            test4.LabelText = "Ohh My";
        }


        // Reroute Mouse events to our UI

        private void mCanvas_MouseButton(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                rootElement.handleMouseInput(e.GetPosition(mCanvas), true);
            }
            else
            {
                rootElement.handleMouseInput(e.GetPosition(mCanvas), false);
            }

            redrawAll();
        }

        private void redrawAll()
        { 
            mCanvas.Children.Clear();

            // This is just a white Rect.. Otherwise the Mouse Move
            // event doesn't get triggered when over "empty" space
            // on the Canvas.. Ignore it
            Rectangle styleRect = new Rectangle();
            styleRect.Fill = new SolidColorBrush(Colors.White);
            styleRect.Width = 800;
            styleRect.Height = 600;
            Canvas.SetLeft(styleRect, 0);
            Canvas.SetTop(styleRect, 0);
            mCanvas.Children.Add(styleRect);

            rootElement.draw(mCanvas, new Point(0, 0));
        }
    }
}
