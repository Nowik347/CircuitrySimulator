using CircuitrySimulator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        public object startWire, endWire;
        public Point point_1, point_2;

        public void PlaceWire(object wire, Canvas placementBoard)
        {
            if (wire == startWire)
                return;

            if (startWire == null)
            {
                startWire = wire;
            }
            else
            {
                endWire = wire;

                if (newElementName != null && newElementName.ToLower() == "subcircuit")
                {
                    if (startWire.GetType().ToString() == "System.Windows.Shapes.Polyline" && endWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        placementBoard.Children.Add(CreateWire(null, true, true));
                    }
                    else if (startWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        placementBoard.Children.Add(CreateWire(null, true, false));
                    }
                    else if (endWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        placementBoard.Children.Add(CreateWire(null, false, true));
                    }
                    else
                    {
                        placementBoard.Children.Add(CreateWire(null, false, false));
                    }
                }
                else 
                {
                    Pathfinder pathfinder = new Pathfinder(placementBoard);

                    if (startWire.GetType().ToString() == "System.Windows.Shapes.Polyline" && endWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        Polyline polyline_1 = startWire as Polyline, polyline_2 = endWire as Polyline;

                        ClosestPoints closestPoints = FindClosest(polyline_1.Points.ToList(), polyline_2.Points.ToList());

                        List<Point> path = pathfinder.FindPath(closestPoints.P1, closestPoints.P2);

                        placementBoard.Children.Add(CreateWire(path, true, true));
                    }
                    else if (startWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        Polyline polyline = startWire as Polyline;
                        Line endLine = endWire as Line;

                        ClosestPoints closestPoints = FindClosest(polyline.Points.ToList(), new List<Point> { new Point(endLine.X2, endLine.Y2) });

                        List<Point> path = pathfinder.FindPath(closestPoints.P1, closestPoints.P2);

                        placementBoard.Children.Add(CreateWire(path, true, false));
                    }
                    else if (endWire.GetType().ToString() == "System.Windows.Shapes.Polyline")
                    {
                        Line startLine = startWire as Line;
                        Polyline polyline = endWire as Polyline;

                        ClosestPoints closestPoints = FindClosest(new List<Point> { new Point(startLine.X2, startLine.Y2) }, polyline.Points.ToList());

                        List<Point> path = pathfinder.FindPath(closestPoints.P1, closestPoints.P2);

                        placementBoard.Children.Add(CreateWire(path, false, true));
                    }
                    else
                    {
                        Line startLine = startWire as Line, endLine = endWire as Line;

                        List<Point> path = pathfinder.FindPath(new Point(startLine.X2, startLine.Y2), new Point(endLine.X2, endLine.Y2));

                        placementBoard.Children.Add(CreateWire(path, false, false));
                    }
                }

                startWire = null;
                endWire = null;
            }
        }

        private Polyline CreateWire(List<Point>? points, bool startPolyline = false, bool endPolyline = false)
        {
            Polyline newWire;

            if (startPolyline && endPolyline)
            {
                Polyline line_1 = startWire as Polyline, line_2 = endWire as Polyline;

                newWire = new Polyline
                {
                    Name = line_1.Name + "Ш" + line_2.Name,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Tag = false,
                };
            }
            else if (startPolyline)
            {
                Polyline line_1 = startWire as Polyline;
                Line line_2 = endWire as Line;

                newWire = new Polyline
                {
                    Name = line_1.Name + "П" + line_2.Name,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Tag = false,
                };
            }
            else if (endPolyline)
            {
                Line line_1 = startWire as Line;
                Polyline line_2 = endWire as Polyline;

                newWire = new Polyline
                {
                    Name = line_1.Name + "П" + line_2.Name,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Tag = false,
                };
            }
            else
            {
                Line line_1 = startWire as Line, line_2 = endWire as Line;

                newWire = new Polyline
                {
                    Name = line_1.Name + "Ф" + line_2.Name,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Tag = false,
                };
            }

            newWire.MouseLeftButtonUp += OnMouseDown;
            newWire.MouseEnter += OnMouseEnter;
            newWire.MouseLeave += OnMouseLeave;

            if (points != null)
                foreach (var point in points)
                    newWire.Points.Add(point);

            if (startPolyline && endPolyline)
            {
                Polyline line_1 = startWire as Polyline, line_2 = endWire as Polyline;

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

                line_1.SetBinding(Polyline.StrokeProperty, colorBinding);
                line_2.SetBinding(Polyline.StrokeProperty, colorBinding);
            }
            else if (startPolyline)
            {
                Polyline line_1 = startWire as Polyline;
                Line line_2 = endWire as Line;

                Binding tagBinding_1 = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                Binding tagBinding_2 = new Binding
                {
                    Source = line_1,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                line_2.SetBinding(TagProperty, tagBinding_1);
                newWire.SetBinding(TagProperty, tagBinding_2);

                Binding colorBinding_1 = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Stroke"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                Binding colorBinding_2 = new Binding
                {
                    Source = line_1,
                    Path = new PropertyPath("Stroke"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                line_2.SetBinding(Line.StrokeProperty, colorBinding_1);
                newWire.SetBinding(Polyline.StrokeProperty, colorBinding_2);
            }
            else if (endPolyline)
            {
                Line line_1 = startWire as Line;
                Polyline line_2 = endWire as Polyline;

                Binding tagBinding_1 = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                Binding tagBinding_2 = new Binding
                {
                    Source = line_2,
                    Path = new PropertyPath("Tag"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                line_1.SetBinding(TagProperty, tagBinding_1);
                newWire.SetBinding(TagProperty, tagBinding_2);

                Binding colorBinding_1 = new Binding
                {
                    Source = newWire,
                    Path = new PropertyPath("Stroke"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                Binding colorBinding_2 = new Binding
                {
                    Source = line_2,
                    Path = new PropertyPath("Stroke"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                };

                line_1.SetBinding(Line.StrokeProperty, colorBinding_1);
                newWire.SetBinding(Polyline.StrokeProperty, colorBinding_2);
            }
            else
            {
                Line line_1 = startWire as Line, line_2 = endWire as Line;

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
            }

            return newWire;
        }

        public sealed class ClosestPoints
        {
            public ClosestPoints(Point p1, Point p2)
            {
                _p1 = p1;
                _p2 = p2;
            }

            public Point P1 { get { return _p1; } }
            public Point P2 { get { return _p2; } }

            public double Distance()
            {
                double dx = P1.X - P2.X;
                double dy = P1.Y - P2.Y;

                return Math.Sqrt(dx * dx + dy * dy);
            }

            private readonly Point _p1;
            private readonly Point _p2;
        }

        private ClosestPoints FindClosest(List<Point> points_1, List<Point> points_2)
        {
            double closest = double.MaxValue;
            ClosestPoints result = null;

            foreach (var p1 in points_1)
            {
                foreach (var p2 in points_2)
                {
                    double dx = p1.X - p2.X;
                    double dy = p1.Y - p2.Y;

                    double distance = dx * dx + dy * dy;

                    if (distance >= closest)
                        continue;

                    result = new ClosestPoints(p1, p2);
                    closest = distance;
                }
            }

            return result;
        }

        //public Line? line_1, line_2;

        //public void PlaceWire(Line line)
        //{
        //    if (line == line_1)
        //        return;

        //    if (line_1 == null)
        //    {
        //        line_1 = line;
        //    }
        //    else
        //    {
        //        line_2 = line;

        //        line_2.StrokeThickness = 1;

        //        Pathfinder pathfinder = new Pathfinder(DrawingBoard);

        //        List<Point> path = pathfinder.FindPath(new Point(line_1.X2, line_1.Y2), new Point(line_2.X2, line_2.Y2));

        //        DrawingBoard.Children.Add(CreateWire(path));

        //        line_1 = null;
        //        line_2 = null;
        //    }
        //}

        //public Polyline? polyline_1, polyline_2;
        //public Point point_1;

        //public void PlaceWire(Polyline polyline, MouseEventArgs e)
        //{
        //    if (polyline == polyline_1)
        //        return;

        //    if (polyline_1 == null)
        //    {
        //        polyline_1 = polyline;
        //        point_1 = new Point(e.GetPosition(DrawingBoard).X, e.GetPosition(DrawingBoard).Y);
        //    }
        //    else
        //    {
        //        polyline_2 = polyline;

        //        polyline_2.StrokeThickness = 1;

        //        Pathfinder pathfinder = new Pathfinder(DrawingBoard);

        //        List<Point> path = pathfinder.FindPath(point_1, new Point(e.GetPosition(DrawingBoard).X, e.GetPosition(DrawingBoard).Y));

        //        DrawingBoard.Children.Add(CreateWire(path));

        //        line_1 = null;
        //        polyline_2 = null;
        //    }
        //}

        //private Polyline CreateWire(List<Point> points, bool mode)
        //{
        //    if (mode == true)
        //    {
        //        Polyline newWire = new Polyline
        //        {
        //            Name = line_1.Name + "Ф" + line_2.Name,
        //            Stroke = new SolidColorBrush(Colors.Black),
        //            StrokeThickness = 2,
        //            Tag = false,
        //        };

        //        newWire.MouseLeftButtonUp += OnMouseDown;
        //        newWire.MouseEnter += OnMouseEnter;
        //        newWire.MouseLeave += OnMouseLeave;

        //        foreach (var point in points)
        //        {
        //            newWire.Points.Add(point);
        //        }

        //        Binding tagBinding = new Binding
        //        {
        //            Source = newWire,
        //            Path = new PropertyPath("Tag"),
        //            Mode = BindingMode.TwoWay,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            NotifyOnSourceUpdated = true,
        //            NotifyOnTargetUpdated = true,
        //        };

        //        line_1.SetBinding(TagProperty, tagBinding);
        //        line_2.SetBinding(TagProperty, tagBinding);

        //        Binding colorBinding = new Binding
        //        {
        //            Source = newWire,
        //            Path = new PropertyPath("Stroke"),
        //            Mode = BindingMode.TwoWay,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            NotifyOnSourceUpdated = true,
        //            NotifyOnTargetUpdated = true,
        //        };

        //        line_1.SetBinding(Line.StrokeProperty, colorBinding);
        //        line_2.SetBinding(Line.StrokeProperty, colorBinding);

        //        return newWire;
        //    }
        //    else if (mode == false)
        //    {
        //        Polyline newWire = new Polyline
        //        {
        //            Name = polyline_1.Name + "Ф" + polyline_2.Name,
        //            Stroke = new SolidColorBrush(Colors.Black),
        //            StrokeThickness = 2,
        //            Tag = false,
        //        };

        //        newWire.MouseLeftButtonUp += OnMouseDown;
        //        newWire.MouseEnter += OnMouseEnter;
        //        newWire.MouseLeave += OnMouseLeave;

        //        foreach (var point in points)
        //        {
        //            newWire.Points.Add(point);
        //        }

        //        Binding tagBinding = new Binding
        //        {
        //            Source = newWire,
        //            Path = new PropertyPath("Tag"),
        //            Mode = BindingMode.TwoWay,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            NotifyOnSourceUpdated = true,
        //            NotifyOnTargetUpdated = true,
        //        };

        //        polyline_1.SetBinding(TagProperty, tagBinding);
        //        polyline_2.SetBinding(TagProperty, tagBinding);

        //        Binding colorBinding = new Binding
        //        {
        //            Source = newWire,
        //            Path = new PropertyPath("Stroke"),
        //            Mode = BindingMode.TwoWay,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            NotifyOnSourceUpdated = true,
        //            NotifyOnTargetUpdated = true,
        //        };

        //        polyline_1.SetBinding(Polyline.StrokeProperty, colorBinding);
        //        polyline_2.SetBinding(Polyline.StrokeProperty, colorBinding);

        //        return newWire;
        //    }
        //    else
        //    {

        //    }

        //}

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
            switch (currentState)
            {
                case "Deletion":
                    DeleteChildObject((Polyline)sender);
                    break;
                case "Wiring":
                    PlaceWire(sender, DrawingBoard);
                    break;
                case "Placement":
                    break;
                default:
                    break;
            }
        }
    }
}
