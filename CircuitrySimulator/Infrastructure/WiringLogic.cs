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
        private enum Direction { Up, Down, Left, Right };
        public Polyline? currentWire;

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

                ConnectOnePoint();

                line_1 = null;
            }
        }

        private void ConnectOnePoint()
        {
            Point startPosition = new Point(line_1.X2, line_1.Y2);
            Point endPosition = new Point(line_2.X2, line_2.Y2);
            Direction startLineDirection = GetLineDirection(line_1);
            Point Point_1;

            if (startLineDirection == Direction.Up || startLineDirection == Direction.Down)
                Point_1 = new Point(startPosition.X, endPosition.Y);
            else
                Point_1 = new Point(endPosition.X, startPosition.Y);

            if (CheckCollision(startPosition, Point_1, endPosition))
                Point_1 = new Point(Point_1.X == startPosition.X ? endPosition.X : startPosition.X, Point_1.Y == startPosition.Y ? endPosition.Y : startPosition.Y);

            if (CheckCollision(startPosition, Point_1, endPosition))
            {
                ConnectTwoPoint();
                return;
            };

            DrawingBoard.Children.Add(CreateWire(new List<Point> { startPosition, Point_1, endPosition }));
        }

        private void ConnectTwoPoint()
        {

        }

        private bool CheckCollision(Point startPosition, Point point, Point endPosition)
        {
            bool collision = false;

            if (startPosition.X == point.X)
            {
                if (startPosition.Y > point.Y)
                {
                    for (double y = startPosition.Y; y > point.Y; y--)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(startPosition.X, y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
                else
                {
                    for (double y = startPosition.Y; y < point.Y; y++)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(startPosition.X, y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
                
                if (endPosition.X > point.X)
                {
                    for (double x = endPosition.X; x < point.X; x++)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, endPosition.Y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
                else
                {
                    for (double x = endPosition.X; x > point.X; x--)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, endPosition.Y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
            }
            else
            {
                if (endPosition.Y > point.Y)
                {
                    for (double y = endPosition.Y; y > point.Y; y--)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(endPosition.X, y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
                else
                {
                    for (double y = endPosition.Y; y < point.Y; y++)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(endPosition.X, y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }

                if (startPosition.X > point.X)
                {
                    for (double x = startPosition.X; x < point.X; x++)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, startPosition.Y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
                else
                {
                    for (double x = startPosition.X; x > point.X; x--)
                    {
                        HitTestResult result = VisualTreeHelper.HitTest(DrawingBoard, new Point(x, startPosition.Y));

                        if (result.VisualHit == line_1 || result.VisualHit == line_2 || result.VisualHit == DrawingBoard)
                            continue;

                        if (result != null)
                            collision = true;
                    }
                }
            }

            return collision;
        }

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

        private Direction GetLineDirection(Line line)
        {
            if (line.X1 == line.X2)
            {
                if (line.Y1 - line.Y2 > 0)
                    return Direction.Up;
                else
                    return Direction.Down;
            }
            else
            {
                if (line.X1 - line.X2 > 0)
                    return Direction.Left;
                else
                    return Direction.Right;
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
