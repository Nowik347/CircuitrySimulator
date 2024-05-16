using System.Globalization;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private Line? startLine, endLine;

        public void PlaceWire(Line line)
        {
            if (line == startLine) 
            {
                startLine = null;
                return;
            }

            if (startLine == null)
            {
                startLine = line;
            }  
            else
            {
                endLine = line;

                Point startPosition = new Point((startLine.X1 + startLine.X2) / 2, (startLine.Y1 + startLine.Y2) / 2), 
                    endPosition = new Point((endLine.X1 + endLine.X2) / 2, (endLine.Y1 + endLine.Y2) / 2);

                int delta = (int)(startPosition.X - endPosition.X);

                Point secondaryPoint = new Point(startPosition.X - delta / 2, startPosition.Y), 
                    tetriaryPoint = new Point(endPosition.X + delta / 2, endPosition.Y);

                Polyline newWire = new Polyline
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Tag = false,
                };

                newWire.MouseLeftButtonUp += OnMouseDown;
                newWire.MouseEnter += OnMouseEnter;
                newWire.MouseLeave += OnMouseLeave;

                //currentSelectedWire = newWire;

                DrawingBoard.Children.Add(newWire);

                newWire.Points.Add((Point)startPosition);
                newWire.Points.Add(secondaryPoint);
                newWire.Points.Add(tetriaryPoint);
                newWire.Points.Add((Point)endPosition);

                Binding bindingColor = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Stroke"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                };

                Binding bindingTag = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                };

                startLine.SetBinding(Line.StrokeProperty, bindingColor);
                endLine.SetBinding(Line.StrokeProperty, bindingColor);

                startLine.SetBinding(Line.TagProperty, bindingTag);
                endLine.SetBinding(Line.TagProperty, bindingTag);

                //Binding inputBinding = new Binding
                //{
                //    Source = startLine,
                //    Path = new PropertyPath("Stroke"),
                //    Mode = BindingMode.OneWay,
                //};

                //Binding ouputBinding = new Binding
                //{
                //    Source = newWire,
                //    Path = new PropertyPath("Stroke"),
                //    Mode = BindingMode.OneWay,
                //};

                //newWire.SetBinding(Polyline.StrokeProperty, inputBinding);
                //endLine.SetBinding(Line.StrokeProperty, ouputBinding);

                startLine = null;
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            Polyline line = sender as Polyline;

            line.StrokeThickness = 2;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Polyline line = sender as Polyline;

            line.StrokeThickness = 5;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentState == "Deletion")
            {
                DeleteChildObject((Polyline)sender);
            }
        }
    }
}
