using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace CircuitrySimulator.Classes
{
    public class Inverter : BaseComponent
    {
        public Inverter(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/inverter_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(1, 1);

            IOLines[1].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[1].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate() 
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Black.ToString())
            {
                IOLines[1].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[1].Tag = true;
            }
            else
            {
                IOLines[1].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[1].Tag = false;
            }
        }
    }
}
