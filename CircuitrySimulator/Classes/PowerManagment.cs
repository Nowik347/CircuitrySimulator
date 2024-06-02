using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace CircuitrySimulator.Classes
{
    class Power : BaseComponent
    {
        public Power(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/power.png", UriKind.Relative));
            Width = 30;
            Height = 30;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            IOLines = CreatePins(0, 1);

            foreach (var item in IOLines)
                ((MainWindow)Application.Current.MainWindow).PlaceChildObject(item);

            IOLines[0].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[0].Tag = false;
        }

        protected override void Simulate()
        {
            base.Simulate();

            if ((bool)((MainWindow)Application.Current.MainWindow).SimulationToggle.IsChecked)
            {
                IOLines[0].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[0].Tag = true;
            }
            else 
            {
                IOLines[0].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[0].Tag = false;
            }
        }
    }

    class Ground : BaseComponent
    {
        public Ground(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/ground.png", UriKind.Relative));
            Width = 30;
            Height = 30;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            IOLines = CreatePins(1, 0);

            foreach (var item in IOLines)
                ((MainWindow)Application.Current.MainWindow).PlaceChildObject(item);

            IOLines[0].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[0].Tag = false;
        }
    }
}
