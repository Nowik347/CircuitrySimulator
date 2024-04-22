using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
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
            labelCorrectionX = -15;
            labelCorrectionY = 25;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            double x1, x2, y1, y2;

            switch (internalRotationAngle)
            {
                case 90:
                    x1 = 30;
                    x2 = 55;
                    y1 = 15;
                    y2 = 15;
                    break;
                case 180:
                    x1 = 15;
                    x2 = 15;
                    y1 = 0;
                    y2 = -25;
                    break;
                case 270:
                    x1 = 0;
                    x2 = -25;
                    y1 = 15;
                    y2 = 15;
                    break;
                default:
                    x1 = 15;
                    x2 = 15;
                    y1 = 30;
                    y2 = 55;
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

            output.MouseLeftButtonUp += ChildrenOnClick;

            output.MouseEnter += ChildrenMouseEnter;

            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(output);

            tempWindow.PlaceChildObject(output);
        }
    }

    class Ground : BaseComponent
    {

    }
}
