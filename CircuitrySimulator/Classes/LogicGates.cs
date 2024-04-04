using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace CircuitrySimulator.Classes
{
    class AND : BaseComponent
    {
        public AND(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/AND.png", UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }
    }

    class OR : BaseComponent
    {
        public OR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/OR.png", UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }
    }

    class NAND : BaseComponent
    {
        public NAND(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/NAND.png", UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }
    }

    class NOR : BaseComponent
    {
        public NOR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/NOR.png", UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }
    }
}
