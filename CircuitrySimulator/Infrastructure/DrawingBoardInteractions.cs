using CircuitrySimulator.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private void DrawingBoard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (drawPreviewImage)
            {
                PreviewImage previewImage = new PreviewImage
                {
                    Name = "previewImage",
                    Source = new BitmapImage(new Uri("../Images/Components/" + newElementName.ToLower() + ".png", UriKind.Relative)),
                    Width = 100,
                    Height = 100,
                    Opacity = 0.5,
                    RenderTransform = new RotateTransform(rotationAngle),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    IsEnabled = false
                };

                Canvas.SetLeft(previewImage, e.GetPosition(DrawingBoard).X - 50);
                Canvas.SetTop(previewImage, e.GetPosition(DrawingBoard).Y - 50);

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
                PlaceObject(e);             
        }

        private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            PositionLabel.Content = e.GetPosition(DrawingBoard);

            // fix logic
            if (drawPreviewImage)
            {
                PreviewImage? previewImage = DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault();

                Canvas.SetLeft(previewImage, e.GetPosition(DrawingBoard).X - 50);
                Canvas.SetTop(previewImage, e.GetPosition(DrawingBoard).Y - 50);
            }
        }

        private void PlaceObject(MouseButtonEventArgs e)
        {
            BaseComponent? newObject = Activator.CreateInstance(Type.GetType("CircuitrySimulator.Classes." + newElementName), new object[] { rotationAngle }) as BaseComponent;

            totalComponentCount++;

            newObject.Name = newElementName + totalComponentCount;

            Canvas.SetLeft(newObject, e.GetPosition(DrawingBoard).X - 50);
            Canvas.SetTop(newObject, e.GetPosition(DrawingBoard).Y - 50);

            DrawingBoard.Children.Add(newObject);
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
