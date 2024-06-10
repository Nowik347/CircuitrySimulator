using CircuitrySimulator.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.F4:
                    PropertiesPanel.Visibility = PropertiesPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case Key.F2:
                    ComponentsPanel.Visibility = ComponentsPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case Key.R:
                    rotationAngle += 90;

                    PreviewImage? previewImage = DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault();

                    if (previewImage != null)
                        previewImage.RenderTransform = new RotateTransform(rotationAngle);
                    break;
                case Key.Delete:
                    if (currentSelectedObject != null)
                    {
                        currentSelectedObject.DeleteChildren();
                        DeleteObject(currentSelectedObject);
                    }
                    else
                    {
                        DeleteToggleButton.IsChecked = true;
                        DeleteToggleButton_Checked(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentState == "Placement")
                DrawingBoard.Children.Remove(DrawingBoard.Children.OfType<PreviewImage>().FirstOrDefault());

            ClearState();

            FocusManager.SetFocusedElement(this, this);

            Keyboard.Focus(this);
        }
    }
}
