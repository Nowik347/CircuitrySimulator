using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CircuitrySimulator.Infrastructure
{
    public class Pathfinder
    {
        private Canvas map;
        private enum Direction { Right, Left, Up, Down }
        private double currentDistanceToEndX, currentDistanceToEndY;
        private Point currentPoint, startPoint, endPoint;
        private Direction? currentDirection;
        private List<Point> path = new List<Point>();

        public Pathfinder(Canvas canvas)
        { 
            map = canvas;
        }

        public List<Point> FindPath(Point start, Point end)
        {
            startPoint = start;
            endPoint = end;

            currentPoint = startPoint;

            CalculateDistance();

            currentDirection = CheckNeighbours();

            path.Add(currentPoint);

            for (int i = 0; i < 10; i++)
            {
                if (currentDistanceToEndX < 1 && currentDistanceToEndY < 1)
                    break;

                bool hitSomething = Follow(currentDirection);

                currentDirection = CheckNeighbours();

                if (hitSomething)
                {
                    if (path.Count < 10)
                    {
                        currentPoint = startPoint;

                        CalculateDistance();
                    }
                    else
                    {
                        for (int j = 0; j < 20; j++)
                            path.Remove(path.Last());

                        currentPoint = path.Last();

                        CalculateDistance();
                    }
                }
            }

            return path;
        }

        private void CalculateDistance()
        {
            currentDistanceToEndX = Math.Abs(endPoint.X - currentPoint.X);
            currentDistanceToEndY = Math.Abs(endPoint.Y - currentPoint.Y);
        }

        private bool Follow(Direction? direction)
        {
            double distance = 10;

            do
            {
                Point newPoint = currentPoint;

                switch (direction)
                {
                    case Direction.Left:
                        newPoint.X--;
                        distance = currentDistanceToEndX;
                        break;
                    case Direction.Right:
                        newPoint.X++;
                        distance = currentDistanceToEndX;
                        break;
                    case Direction.Up:
                        newPoint.Y--;
                        distance = currentDistanceToEndY;
                        break;
                    case Direction.Down:
                        newPoint.Y++;
                        distance = currentDistanceToEndY;
                        break;
                }

                if (CheckCollision(newPoint))
                    return true;

                currentPoint = newPoint;

                path.Add(currentPoint);

                CalculateDistance();
            } while (distance > 1);

            return false;
        }

        private bool CheckCollision(Point point)
        {
            HitTestResult result = VisualTreeHelper.HitTest(map, point);

            object hitObject = result.VisualHit;

            if (result.VisualHit.ToString() != "System.Windows.Controls.Canvas" && result.VisualHit.ToString() != "System.Windows.Shapes.Polyline" && hitObject != ((MainWindow)Application.Current.MainWindow).endWire)
                return true;
            else
                return false;
        }

        private Direction? CheckNeighbours()
        {
            int[,] modifiers = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 1 } };

            for (int i = 0; i < 4; i++)
            {
                Point neighbour = new Point(currentPoint.X + modifiers[i, 0], currentPoint.Y + modifiers[i, 1]);

                if (!CheckCollision(neighbour))
                    if ((i < 2 ? Math.Abs(endPoint.X - neighbour.X) : Math.Abs(endPoint.Y - neighbour.Y)) < (i < 2 ? currentDistanceToEndX : currentDistanceToEndY))
                        return (Direction)i;
            }

            //Point neighbour = new Point( currentPoint.X + 1, currentPoint.Y);

            //if (CheckCollision(neighbour))  
            //    if (Math.Abs(endPoint.X - neighbour.X) < currentDistanceToEndX)
            //        return Direction.Right;

            //neighbour = new Point(currentPoint.X - 1, currentPoint.Y);

            //if (CheckCollision(neighbour))
            //    if (Math.Abs(endPoint.X - neighbour.X) < currentDistanceToEndX)
            //        return Direction.Left;

            //neighbour = new Point(currentPoint.X, currentPoint.Y - 1);

            //if (CheckCollision(neighbour))
            //    if (Math.Abs(endPoint.Y - neighbour.Y) < currentDistanceToEndY)
            //        return Direction.Up;

            //neighbour = new Point(currentPoint.X, currentPoint.Y + 1);

            //if (CheckCollision(neighbour))
            //    if (Math.Abs(endPoint.Y - neighbour.Y) < currentDistanceToEndY)
            //        return Direction.Down;

            return currentDirection;
        }
    }
}
