using CircuitrySimulator.Classes;
using System;
using System.Collections.Generic;
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

        public Pathfinder(Canvas canvas)
        { 
            map = canvas;
        }

        public List<Point> FindPath(Point start, Point end)
        {
            startPoint = start;
            endPoint = end;

            currentPoint = startPoint;

            currentDistanceToEndX = Math.Abs(endPoint.X - currentPoint.X);
            currentDistanceToEndY = Math.Abs(endPoint.Y - currentPoint.Y);

            List<Point> path = new List<Point>();

            currentDirection = CheckNeighbours();

            for (int i = 0; i < 10000; i++)
            {
                path.Add(currentPoint);

                currentPoint = Step(currentDirection);

                currentDistanceToEndX = Math.Abs(endPoint.X - currentPoint.X);
                currentDistanceToEndY = Math.Abs(endPoint.Y - currentPoint.Y);

                if (currentDistanceToEndX < 1 && currentDistanceToEndY < 1)
                    break;

                currentDirection = CheckNeighbours();
            }

            return path;
        }

        private Point Step(Direction? direction)
        {
            Point newPoint = currentPoint;

            switch (direction)
            {
                case Direction.Left:
                    newPoint.X--;
                    break;
                case Direction.Right:
                    newPoint.X++;
                    break;
                case Direction.Up:
                    newPoint.Y--;
                    break;
                case Direction.Down:
                    newPoint.Y++;
                    break;
            }

            return newPoint;
        }

        private bool CheckCollision(Point point)
        {
            HitTestResult result = VisualTreeHelper.HitTest(map, point);

            if (result.VisualHit.ToString() != "System.Windows.Controls.Canvas")
                return false;
            else
                return true;
        }

        private Direction? CheckNeighbours()
        {
            Point neighbour = new Point( currentPoint.X + 1, currentPoint.Y);

            if (CheckCollision(neighbour))  
                if (Math.Abs(endPoint.X - neighbour.X) < currentDistanceToEndX)
                    return Direction.Right;

            neighbour = new Point(currentPoint.X - 1, currentPoint.Y);

            if (CheckCollision(neighbour))
                if (Math.Abs(endPoint.X - neighbour.X) < currentDistanceToEndX)
                    return Direction.Left;

            neighbour = new Point(currentPoint.X, currentPoint.Y - 1);

            if (CheckCollision(neighbour))
                if (Math.Abs(endPoint.Y - neighbour.Y) < currentDistanceToEndY)
                    return Direction.Up;

            neighbour = new Point(currentPoint.X, currentPoint.Y + 1);

            if (CheckCollision(neighbour))
                if (Math.Abs(endPoint.Y - neighbour.Y) < currentDistanceToEndY)
                    return Direction.Down;

            return  currentDirection;
        }
    }
}
