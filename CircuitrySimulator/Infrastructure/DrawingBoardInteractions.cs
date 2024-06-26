﻿using CircuitrySimulator.Classes;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private int _rotationAngle;

        public int rotationAngle
        {
            get { return _rotationAngle; }

            set { _rotationAngle = _rotationAngle == 270 ? 0 : value; }
        }

        public string? newElementName;
        public BaseComponent? currentSelectedObject;

        private uint totalComponentCount = 0;
        private bool drawPreviewImage;

        private void DrawingBoard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (drawPreviewImage)
            {
                PreviewImage previewImage;

                if (newElementName.ToLower() != "power" && newElementName.ToLower() != "ground" && newElementName.ToLower() != "subcircuitpin")
                {
                    if (newElementName.ToLower() == "subcircuit")
                    {
                        previewImage = new PreviewImage
                        {
                            Name = "previewImage",
                            Source = new BitmapImage(new Uri("../Images/Components/Edited/" + newElementName.ToLower() + "_edited.png", UriKind.Relative)),
                            Width = 70,
                            Height = 70,
                            Opacity = 0.5,
                            RenderTransform = new RotateTransform(rotationAngle),
                            RenderTransformOrigin = new Point(0.5, 0.5),
                            IsEnabled = false,
                        };
                    }
                    else
                    {
                        previewImage = new PreviewImage
                        {
                            Name = "previewImage",
                            Source = new BitmapImage(new Uri("../Images/Components/Edited/" + newElementName.ToLower() + "_edited.png", UriKind.Relative)),
                            Width = 50,
                            Height = 50,
                            Opacity = 0.5,
                            RenderTransform = new RotateTransform(rotationAngle),
                            RenderTransformOrigin = new Point(0.5, 0.5),
                            IsEnabled = false,
                        };
                    }
                }
                else
                {
                    previewImage = new PreviewImage
                    {
                        Name = "previewImage",
                        Source = new BitmapImage(new Uri("../Images/Components/Edited/" + newElementName.ToLower() + "_edited.png", UriKind.Relative)),
                        Width = 30,
                        Height = 30,
                        Opacity = 0.5,
                        RenderTransform = new RotateTransform(rotationAngle),
                        RenderTransformOrigin = new Point(0.5, 0.5),
                        IsEnabled = false,
                    };
                }

                Canvas.SetLeft(previewImage, e.GetPosition(DrawingBoard).X - previewImage.Width / 2);
                Canvas.SetTop(previewImage, e.GetPosition(DrawingBoard).Y - previewImage.Height / 2);

                DrawingBoard.Children.Add(previewImage);
            }
        }

        private void DrawingBoard_MouseLeave(object sender, MouseEventArgs e)
        {
            if (currentState == "Placement")
                DrawingBoard.Children.Remove(DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault());
        }

        private void DrawingBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentState == "Placement")
                PlaceObject(e.GetPosition(DrawingBoard).X, e.GetPosition(DrawingBoard).Y);
        }

        private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            PositionLabel.Content = e.GetPosition(DrawingBoard);

            // fix logic
            if (drawPreviewImage)
            {
                PreviewImage? previewImage = DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault();

                Canvas.SetLeft(previewImage, e.GetPosition(DrawingBoard).X - previewImage.Width / 2);
                Canvas.SetTop(previewImage, e.GetPosition(DrawingBoard).Y - previewImage.Height / 2);
            }
        }

        private void PlaceObject(double x, double y)
        {
            BaseComponent? newObject = null;

            //if (newElementName == "SubCircuit")
            //    newObject = Activator.CreateInstance(Type.GetType("CircuitrySimulator.Classes." + newElementName), new object[] { rotationAngle, }) as BaseComponent;
            //else
            newObject = Activator.CreateInstance(Type.GetType("CircuitrySimulator.Classes." + newElementName), new object[] { rotationAngle }) as BaseComponent;

            StringBuilder builder = new StringBuilder();
            Enumerable.Range(65, 26).Select(e => ((char)e).ToString()).Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString())).OrderBy(e => Guid.NewGuid()).Take(9).ToList().ForEach(e => builder.Append(e));

            newObject.Name = newElementName + "_" + builder.ToString();

            Canvas.SetLeft(newObject, x - newObject.Width / 2);
            Canvas.SetTop(newObject, y - newObject.Height / 2);

            DrawingBoard.Children.Add(newObject);

            totalComponentCount++;
        }

        public void PlaceChildObject(UIElement child)
        {
            DrawingBoard.Children.Add(child);
        }

        public void DeleteChildObject(UIElement child)
        {
            DrawingBoard.Children.Remove(child);
        }

        public void DeleteObject(UIElement element)
        {
            DrawingBoard.Children.Remove(element);
            totalComponentCount--;
        }
    }
}
