using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CircuitrySimulator.Classes
{
    class Inverter : BaseComponent
    {
        public Inverter(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/inverter_edited.png", UriKind.Relative));
            Width = 70;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            labelCorrectionX = 0;
            labelCorrectionY = 20;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            double x1, x2, y1, y2;

            switch (internalRotationAngle)
            {
                case 90:
                    x1 = 36;
                    x2 = 36;
                    y1 = -15;
                    y2 = 10;
                    break;
                case 180:
                    x1 = 60;
                    x2 = 85;
                    y1 = 36;
                    y2 = 36;
                    break;
                case 270:
                    x1 = 34;
                    x2 = 34;
                    y1 = 60;
                    y2 = 85;
                    break;
                default:
                    x1 = -15;
                    x2 = 10;
                    y1 = 34;
                    y2 = 34;
                    break;
            }

            Line input = new Line
            {
                Name = this.Name + "_input",
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + x1,
                X2 = Canvas.GetLeft(this) + x2,
                Y1 = Canvas.GetTop(this) + y1,
                Y2 = Canvas.GetTop(this) + y2,
            };

            Binding binding = new Binding
            {
                Source = this,
                Path = new PropertyPath("EmpyProperty"),
                Mode = BindingMode.TwoWay,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            input.SetBinding(Line.StrokeProperty, binding);

            input.Stroke = new SolidColorBrush(Colors.Black);

            switch (internalRotationAngle)
            {
                case 90:
                    y1 = 60;
                    y2 = 85;
                    break;
                case 180:
                    x1 = -15;
                    x2 = 10;
                    break;
                case 270:
                    y1 = -15;
                    y2 = 10;
                    break;
                default:
                    x1 = 60;
                    x2 = 85;
                    break;
            }

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + x1,
                X2 = Canvas.GetLeft(this) + x2,
                Y1 = Canvas.GetTop(this) + y1,
                Y2 = Canvas.GetTop(this) + y2,
            };

            input.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input);
            tempWindow.PlaceChildObject(output);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (this.IsInitialized)
                Simulate();
        }

        protected override void Simulate() 
        {
            if (IOLines[0].Stroke.ToString() == Colors.Black.ToString())
                IOLines[1].Stroke = new SolidColorBrush(Colors.Green);
            else
                IOLines[1].Stroke = new SolidColorBrush(Colors.Black);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            IOLines[1].Stroke = new SolidColorBrush(Colors.Green);
        }
    }
}
