using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;

namespace CircuitrySimulator.Classes
{
    public class Inverter : BaseComponent
    {
        private readonly int[,,] pinX =
        {
            {
                {-15, 36, 60, 34}, {10, 36, 85, 34}
            },
            {
                {60, 36, -15, 34}, {85, 36, 10, 34}
            }
        };

        private readonly int[,,] pinY =
        {
            {
                {34, -15, 36, 60}, {34, 10, 36, 85}
            },
            {
                {34, 60, 36, -15}, {34, 85, 36, 10}
            }
        };

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

        public Inverter() { }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input = new Line
            {
                Name = this.Name + "_input",
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + pinX[0, 0, (int)(internalRotationAngle / 90)],
                X2 = Canvas.GetLeft(this) + pinX[0, 1, (int)(internalRotationAngle / 90)],
                Y1 = Canvas.GetTop(this) + pinY[0, 0, (int)(internalRotationAngle / 90)],
                Y2 = Canvas.GetTop(this) + pinY[0, 1, (int)(internalRotationAngle / 90)],
            };

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + pinX[1, 0, (int)(internalRotationAngle / 90)],
                X2 = Canvas.GetLeft(this) + pinX[1, 1, (int)(internalRotationAngle / 90)],
                Y1 = Canvas.GetTop(this) + pinY[1, 0, (int)(internalRotationAngle / 90)],
                Y2 = Canvas.GetTop(this) + pinY[1, 1, (int)(internalRotationAngle / 90)],
            };

            Binding binding = new Binding
            {
                Source = input,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding);

            input.Stroke = new SolidColorBrush(Colors.Black);

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

        protected override void Simulate() 
        {
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
