using CircuitrySimulator.Infrastructure;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private Line? line_1, line_2;

        public void PlaceWire(Line line)
        {
            if (line == line_1)
                return;

            if (line_1 == null)
            {
                line_1 = line;
            }
            else
            {
                line_2 = line;

                Pathfinder pathfinder = new Pathfinder(DrawingBoard);

                List<Point> path = pathfinder.FindPath(new Point(line_1.X2, line_1.Y2), new Point(line_2.X2, line_2.Y2));

                DrawingBoard.Children.Add(CreateWire(path));

                line_1 = null;
                line_2 = null;
            }
        }

        //private List<List<Node>> CalculateBoardPathingMap()
        //{
        //    List<List<Node>> boardPathingMap = new List<List<Node>>();

        //    for (int y = 0; y < DrawingBoard.ActualHeight; y++)
        //    {
        //        List<Node> mapLine = new List<Node>();

        //        for (int x = 0; x < DrawingBoard.ActualWidth; x++)
        //        {
        //            HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, y));

        //            if (result.VisualHit.ToString() != "System.Windows.Controls.Canvas")
        //                mapLine.Add(new Node(new Vector2(x, y), false, 0));
        //            else
        //                mapLine.Add(new Node(new Vector2(x, y), true , 0));
        //        }

        //        boardPathingMap.Add(mapLine);
        //    }

        //    return boardPathingMap;
        //}

        //private void CalculatePathingMap()
        //{
        //    if (line_1.X2
        //    for (double y = line_1.X2; y < DrawingBoard.ActualHeight; y++)
        //    {
        //        for (double x = line_1.Y2; x < DrawingBoard.ActualWidth; x++)
        //        {
        //            HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, y));

        //            if (result != null)
        //                boardPathingMap[(short)y, (short)x] = 1;
        //            else
        //                boardPathingMap[(short)y, (short)x] = 1;
        //        }
        //    }
        //}

        private Polyline CreateWire(List<Point> points)
        {
            Polyline newWire = new Polyline
            {
                Name = line_1.Name + "Ф" + line_2.Name,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2,
                Tag = false,
            };

            newWire.MouseLeftButtonUp += OnMouseDown;
            newWire.MouseEnter += OnMouseEnter;
            newWire.MouseLeave += OnMouseLeave;

            foreach (var point in points)
            {
                newWire.Points.Add(point);
            }

            Binding tagBinding = new Binding
            {
                Source = newWire,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            line_1.SetBinding(TagProperty, tagBinding);
            line_2.SetBinding(TagProperty, tagBinding);

            Binding colorBinding = new Binding
            {
                Source = newWire,
                Path = new PropertyPath("Stroke"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            line_1.SetBinding(Line.StrokeProperty, colorBinding);
            line_2.SetBinding(Line.StrokeProperty, colorBinding);

            return newWire;
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
