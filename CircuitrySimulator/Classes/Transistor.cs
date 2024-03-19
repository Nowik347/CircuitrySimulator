using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    internal class Transistor : Image
    {
        Rectangle? selectionFrame;

        public Transistor(double rotationAngle, string imageSource) 
        {
            Name = "element";
            Source = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            double x = Canvas.GetLeft(this);
            double y = Canvas.GetTop(this);

            var tempWindow = (MainWindow)Application.Current.MainWindow;

            selectionFrame = tempWindow.Draw_Selection_Frame(x, y);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            var tempWindow = (MainWindow)Application.Current.MainWindow;

            tempWindow.Remove_Selection_Frame(selectionFrame);

            selectionFrame = null;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
        }
    }
}
