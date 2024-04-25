using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CircuitrySimulator.Classes
{
    class Transistor : BaseComponent
    {
        public Transistor(double rotationAngle) 
        {
            Source = new BitmapImage(new Uri("../Images/Components/transistor.png", UriKind.Relative));
            Width = 100;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input1 = new Line
            {
                Name = this.Name + "_input1",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this),
                X2 = Canvas.GetLeft(this) + 20,
                Y1 = Canvas.GetTop(this) + 76,
                Y2 = Canvas.GetTop(this) + 76,            
            };

            Line input2 = new Line
            {
                Name = this.Name + "_input2",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 50,
                X2 = Canvas.GetLeft(this) + 50,
                Y1 = Canvas.GetTop(this),
                Y2 = Canvas.GetTop(this) + 24,
            };

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 80,
                X2 = Canvas.GetLeft(this) + 100,
                Y1 = Canvas.GetTop(this) + 76,
                Y2 = Canvas.GetTop(this) + 76,
            };

            input1.MouseLeftButtonUp += ChildrenOnClick;
            input2.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input1.MouseEnter += ChildrenMouseEnter;
            input2.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input1.MouseLeave += ChildrenMouseLeave;
            input2.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input1);
            IOLines.Add(input2);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input1);
            tempWindow.PlaceChildObject(input2);
            tempWindow.PlaceChildObject(output);
        }
    }
}
