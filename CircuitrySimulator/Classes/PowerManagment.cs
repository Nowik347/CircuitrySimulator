using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace CircuitrySimulator.Classes
{
    class Power : BaseComponent
    {
        private readonly int[,] pinX =
        {
            {30, 15, 0, 15}, {55, 15, -25, 15}
        };

        private readonly int[,] pinY =
        {
            {15, 0, 15, 30}, {15, -25, 15, 55}
        };

        public Power(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/power.png", UriKind.Relative));
            Width = 30;
            Height = 30;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            labelCorrectionX = -15;
            labelCorrectionY = 25;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + pinX[0, (int)(internalRotationAngle / 90)],
                X2 = Canvas.GetLeft(this) + pinX[1, (int)(internalRotationAngle / 90)],
                Y1 = Canvas.GetTop(this) + pinY[0, (int)(internalRotationAngle / 90)],
                Y2 = Canvas.GetTop(this) + pinY[1, (int)(internalRotationAngle / 90)],
            };

            output.MouseLeftButtonUp += ChildrenOnClick;

            output.MouseEnter += ChildrenMouseEnter;

            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(output);

            tempWindow.PlaceChildObject(output);
        }

        protected override void Simulate()
        {
            if ((bool)tempWindow.SimulationToggle.IsChecked)
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
        private readonly int[,] pinX =
        {
            {30, 15, 0, 15}, {55, 15, -25, 15}
        };

        private readonly int[,] pinY =
        {
            {15, 0, 15, 30}, {15, -25, 15, 55}
        };

        public Ground(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/ground.png", UriKind.Relative));
            Width = 30;
            Height = 30;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            labelCorrectionX = -15;
            labelCorrectionY = 25;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + pinX[0, (int)(internalRotationAngle / 90)],
                X2 = Canvas.GetLeft(this) + pinX[1, (int)(internalRotationAngle / 90)],
                Y1 = Canvas.GetTop(this) + pinY[0, (int)(internalRotationAngle / 90)],
                Y2 = Canvas.GetTop(this) + pinY[1, (int)(internalRotationAngle / 90)],
            };

            output.MouseLeftButtonUp += ChildrenOnClick;

            output.MouseEnter += ChildrenMouseEnter;

            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(output);

            tempWindow.PlaceChildObject(output);
        }
    }
}
