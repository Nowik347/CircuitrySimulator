using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator.Classes
{
    abstract public partial class BaseComponent : Image
    {
        public void ChildrenOnClick(object sender, MouseButtonEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).currentState == "Wiring")
                ((MainWindow)Application.Current.MainWindow).PlaceWire(sender);
        }

        public void ChildrenMouseEnter(object sender, MouseEventArgs e)
        {
            Line line = sender as Line;
            line.StrokeThickness = 5;
        }

        public void ChildrenMouseLeave(object sender, MouseEventArgs e)
        {
            Line line = sender as Line;
            line.StrokeThickness = 1;
        }

        public List<Line> CreatePins(int numberOfInputs, int numberOfOutputs)
        {
            List<Line> newIO = new List<Line>();

            if (numberOfInputs > 0)
                CreateInputs(newIO, numberOfInputs);

            if (numberOfOutputs > 0)
                CreateOutputs(newIO, numberOfOutputs);

            return newIO;
        }

        private List<Line> CreateInputs(List<Line>newIO, int numberOfInputs)
        {
            double currentWidth = 0, currentHeight = 0, horizontalSpacing = (this.Width / numberOfInputs) / 2, verticalSpacing = (this.Height / numberOfInputs) / 2;

            MultiBinding pinBindings = new MultiBinding();

            for (int i = numberOfInputs; i > 0; i--)
            {
                Line input = new Line
                {
                    Name = this.Name + "_input" + i,
                    StrokeThickness = 1,
                    Tag = false,
                };

                switch (internalRotationAngle)
                {
                    case 90:
                        input.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        input.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        input.Y1 = Canvas.GetTop(this) + 2;
                        input.Y2 = Canvas.GetTop(this) - 23;
                        currentWidth += horizontalSpacing * 2;
                        break;
                    case 180:
                        input.X1 = Canvas.GetLeft(this) + this.Width - 2;
                        input.X2 = Canvas.GetLeft(this) + this.Width + 23;
                        input.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        input.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        currentHeight += verticalSpacing * 2;
                        break;
                    case 270:
                        input.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        input.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        input.Y1 = Canvas.GetTop(this) + this.Height - 2;
                        input.Y2 = Canvas.GetTop(this) + this.Height + 23;
                        currentWidth += horizontalSpacing * 2;
                        break;
                    default:
                        input.X1 = Canvas.GetLeft(this) + 2;
                        input.X2 = Canvas.GetLeft(this) - 23;
                        input.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        input.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        currentHeight += verticalSpacing * 2;
                        break;
                }

                Binding binding = new Binding
                {
                    Source = input,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                pinBindings.Bindings.Add(binding);

                input.Stroke = new SolidColorBrush(Colors.Black);

                input.MouseLeftButtonUp += ChildrenOnClick;

                input.MouseEnter += ChildrenMouseEnter;

                input.MouseLeave += ChildrenMouseLeave;

                newIO.Add(input);
            }

            pinBindings.Converter = new PinConverter();

            this.SetBinding(Image.TagProperty, pinBindings);

            return newIO;
        }

        private List<Line> CreateOutputs(List<Line> newIO, int numberOfOutputs)
        {
            double currentWidth = 0, currentHeight = 0, horizontalSpacing = (this.Width / numberOfOutputs) / 2, verticalSpacing = (this.Height / numberOfOutputs) / 2;

            for (int i = numberOfOutputs; i > 0; i--)
            {
                Line output = new Line
                {
                    Name = this.Name + "_output" + i,
                    StrokeThickness = 1,
                    Tag = false,
                };

                switch (internalRotationAngle)
                {
                    case 90:
                        output.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        output.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        output.Y1 = this.Height + Canvas.GetTop(this) - 2;
                        output.Y2 = this.Height + Canvas.GetTop(this) + 23;
                        currentWidth += horizontalSpacing * 2;
                        break;
                    case 180:
                        output.X1 = Canvas.GetLeft(this) + 2;
                        output.X2 = Canvas.GetLeft(this) - 23;
                        output.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        output.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        currentHeight += verticalSpacing * 2;
                        break;
                    case 270:
                        output.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        output.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
                        output.Y1 = Canvas.GetTop(this) + 2;
                        output.Y2 = Canvas.GetTop(this) - 23;
                        currentWidth += horizontalSpacing * 2;
                        break;
                    default:
                        output.X1 = this.Width + Canvas.GetLeft(this) - 2;
                        output.X2 = this.Width + Canvas.GetLeft(this) + 23;
                        output.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        output.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
                        currentHeight += verticalSpacing * 2;
                        break;
                }

                output.MouseLeftButtonUp += ChildrenOnClick;

                output.MouseEnter += ChildrenMouseEnter;

                output.MouseLeave += ChildrenMouseLeave;

                newIO.Add(output);
            }

            return newIO;
        }
    }
}
