using CircuitrySimulator.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private void clearState()
        {
            currentState = State.Selection;

            StatusLabel.Content = StatusLabelStates[(int)currentState];
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F4:
                    PropertiesPanel.Visibility = PropertiesPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case Key.R:
                    if (currentState != State.Selection)
                    {
                        rotationAngle += 90;

                        Image previewImage = DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault();

                        if (previewImage != null) 
                            previewImage.RenderTransform = new RotateTransform(rotationAngle);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentState != State.Selection)
                DrawingBoard.Children.Remove(DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault());

            clearState();

            FocusManager.SetFocusedElement(this, null);

            Keyboard.ClearFocus();
        }

        public Rectangle Draw_Selection_Frame(double x, double y)
        {
            Rectangle selectionFrame = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = null,
                StrokeThickness = 2,
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection() { 4, 1 }
            };

            Canvas.SetLeft(selectionFrame, x);
            Canvas.SetTop(selectionFrame, y);

            DrawingBoard.Children.Add(selectionFrame);

            currentState = State.Transistor;

            StatusLabel.Content = StatusLabelStates[(int)currentState];

            return selectionFrame;
        }

        public void Remove_Selection_Frame(Rectangle? selectionFrame)
        {
            DrawingBoard.Children.Remove(selectionFrame);
            clearState();
        }
    }
}
