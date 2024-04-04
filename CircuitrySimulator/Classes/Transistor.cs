﻿using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

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
    }
}
