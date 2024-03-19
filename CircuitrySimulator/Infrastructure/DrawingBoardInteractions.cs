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
            if (currentState != State.Selection)
            {     
                PreviewImage previewImage = new PreviewImage
                {
                    Name = "previewImage",
                    Source = new BitmapImage(new Uri(PreviewImagesSources[(int)currentState], UriKind.Relative)),
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
            if (currentState != State.Selection)
                DrawingBoard.Children.Remove(DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault());
        }

        private void DrawingBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (currentState)
            {
                case State.Transistor:
                    Transistor newElement = new Transistor(rotationAngle, PreviewImagesSources[(int)currentState]);

                    Canvas.SetLeft(newElement, e.GetPosition(DrawingBoard).X - 50);
                    Canvas.SetTop(newElement, e.GetPosition(DrawingBoard).Y - 50);

                    DrawingBoard.Children.Add(newElement);
                    break;
                case State.Selection:
                    break;
            }
        }

        private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            PositionLabel.Content = e.GetPosition(DrawingBoard);
            
            // fix logic
            if (currentState != State.Selection)
            {
                Image previewImage = DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault();

                Canvas.SetLeft(previewImage, e.GetPosition(DrawingBoard).X - 50);
                Canvas.SetTop(previewImage, e.GetPosition(DrawingBoard).Y - 50);
            }
        }
    }
}
