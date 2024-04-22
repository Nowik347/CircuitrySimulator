using CircuitrySimulator.Classes;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string currentState;

        public MainWindow()
        {
            InitializeComponent();

            ClearState();
        }

        private void SetState_FromButton(object sender, RoutedEventArgs e)
        {
            string senderName = ((Button)sender).Name.ToString().Trim();

            newElementName = senderName.Remove(senderName.Length - 6);

            currentState = "Placement";

            UpdateStatusLabel();

            SelectionToggleButton.IsChecked = false;
            DeleteToggleButton.IsChecked = false;
            WiringToggleButton.IsChecked = false;

            drawPreviewImage = true;
        }

        private void ClearState(object? sender = null, RoutedEventArgs? e = null)
        {
            currentState = "Selection";

            SelectionToggleButton.IsChecked = true;

            DeleteToggleButton.IsChecked = false;
            WiringToggleButton.IsChecked = false;
            drawPreviewImage = false;

            UpdateStatusLabel();
        }

        public void UpdateStatusLabel()
        {
            StatusLabel.Content = currentState;
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

            return selectionFrame;
        }

        public void Remove_Selection_Frame(Rectangle? selectionFrame)
        {
            DrawingBoard.Children.Remove(selectionFrame);
            ClearState();
        }

        private void DeleteToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (currentState == "Deletion")
            {
                ClearState();
                return;
            }

            SelectionToggleButton.IsChecked = false;
            WiringToggleButton.IsChecked = false;
            drawPreviewImage = false;

            currentState = "Deletion";
            UpdateStatusLabel();
        }

        private void WiringToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (currentState == "Wiring")
            {
                ClearState();
                return;
            }

            SelectionToggleButton.IsChecked = false;
            DeleteToggleButton.IsChecked = false;
            drawPreviewImage = false;

            currentState = "Wiring";
            UpdateStatusLabel();
        }
    }
}
