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
            Source = new BitmapImage(new Uri("../Images/Components/Edited/AND_edited.png", UriKind.Relative));
            Height = 50;
            Width = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(2, 1);

            IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[2].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() && IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }

    class OR : BaseComponent
    {
        public OR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/OR_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(2, 1);

            IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[2].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() || IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }

    class NAND : BaseComponent
    {
        public NAND(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/NAND_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(2, 1);

            IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[2].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate()
        {
            base.Simulate();
            
            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() && IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = false;
            }
        }
    }

    class NOR : BaseComponent
    {
        public NOR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/NOR_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(2, 1);

            IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[2].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() || IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = false;
            }
        }
    }

    class XOR : BaseComponent
    {
        public XOR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/XOR_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(2, 1);

            IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[2].Tag = false;

            base.OnInitialized(e);
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() && IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
            else if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() || IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }
}
