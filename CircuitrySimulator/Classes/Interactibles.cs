using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace CircuitrySimulator.Classes
{
    public class Diode : BaseComponent
    {
        private Ellipse? ellipse;

        public Diode(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/diode_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
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

            ellipse = new Ellipse 
            { 
                Width = 44,
                Height = 44,
                Stroke = new SolidColorBrush(Colors.Red),
                Fill = new SolidColorBrush(Colors.Red),
            };

            childrenElements.Add(ellipse);

            Canvas.SetLeft(ellipse, Canvas.GetLeft(this) + 3);
            Canvas.SetTop(ellipse, Canvas.GetTop(this) + 3);

            ((MainWindow)Application.Current.MainWindow).PlaceChildObject(ellipse);
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (IOLines[0].Stroke.ToString() == Colors.Black.ToString())
            {
                ellipse.Fill = new SolidColorBrush(Colors.Red);
                ellipse.Stroke = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ellipse.Fill = new SolidColorBrush(Colors.Green);
                ellipse.Stroke = new SolidColorBrush(Colors.Green);
            }
        }
    }

    public class Button : BaseComponent
    {
        private Ellipse? ellipse;

        public Button(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/button_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            IOLines = CreatePins(1, 1);

            foreach (var item in IOLines)
                ((MainWindow)Application.Current.MainWindow).PlaceChildObject(item);

            ellipse = new Ellipse
            {
                Width = 30,
                Height = 30,
                Stroke = new SolidColorBrush(Colors.Red),
                Fill = new SolidColorBrush(Colors.Red),
            };

            childrenElements.Add(ellipse);

            Canvas.SetLeft(ellipse, Canvas.GetLeft(this) + 10);
            Canvas.SetTop(ellipse, Canvas.GetTop(this) + 10);

            ((MainWindow)Application.Current.MainWindow).PlaceChildObject(ellipse);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            ellipse.Fill = new SolidColorBrush(Colors.Green);
            ellipse.Stroke = new SolidColorBrush(Colors.Green);

            this.Tag = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            ellipse.Fill = new SolidColorBrush(Colors.Red);
            ellipse.Stroke = new SolidColorBrush(Colors.Red);

            this.Tag = false;
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (ellipse.Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[1].Stroke = IOLines[0].Stroke;
                IOLines[1].Tag = IOLines[0].Tag;
            }
            else
            {
                IOLines[1].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[1].Tag = false;
            }
        }
    }

    public class Switch : BaseComponent
    {
        private Ellipse? ellipse;

        public Switch(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/button_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            IOLines = CreatePins(1, 1);

            foreach (var item in IOLines)
                ((MainWindow)Application.Current.MainWindow).PlaceChildObject(item);

            ellipse = new Ellipse
            {
                Width = 30,
                Height = 30,
                Stroke = new SolidColorBrush(Colors.Red),
                Fill = new SolidColorBrush(Colors.Red),
            };

            childrenElements.Add(ellipse);

            Canvas.SetLeft(ellipse, Canvas.GetLeft(this) + 10);
            Canvas.SetTop(ellipse, Canvas.GetTop(this) + 10);

            ((MainWindow)Application.Current.MainWindow).PlaceChildObject(ellipse);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (ellipse.Stroke.ToString() == Colors.Green.ToString())
            {
                ellipse.Fill = new SolidColorBrush(Colors.Red);
                ellipse.Stroke = ellipse.Fill;
            }
            else
            {
                ellipse.Fill = new SolidColorBrush(Colors.Green);
                ellipse.Stroke = ellipse.Fill;
            }

            this.Tag = true;
        }

        protected override void Simulate()
        {
            base.Simulate();

            if (ellipse.Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[1].Stroke = IOLines[0].Stroke;
                IOLines[1].Tag = IOLines[0].Tag;
            }
            else
            {
                IOLines[1].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[1].Tag = false;
            }
        }
    }

    public class Oscilator : BaseComponent
    {
        private Ellipse? ellipse;

        public Oscilator(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/oscilator_edited.png", UriKind.Relative));
            Width = 50;
            Height = 50;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
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

            //if (IOLines[0].Stroke.ToString() == Colors.Black.ToString())
            //{
            //    ellipse.Fill = new SolidColorBrush(Colors.Red);
            //    ellipse.Stroke = new SolidColorBrush(Colors.Red);
            //}
            //else
            //{
            //    ellipse.Fill = new SolidColorBrush(Colors.Green);
            //    ellipse.Stroke = new SolidColorBrush(Colors.Green);
            //}
        }
    }
}
