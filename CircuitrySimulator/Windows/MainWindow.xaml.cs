using CircuitrySimulator.Classes;
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
            string senderName = "";

            if (sender.GetType().ToString() != "System.Windows.Controls.Button")
            {
                senderName = ((System.Windows.Controls.MenuItem)sender).Name.ToString().Trim();
            }
            else
            {
                senderName = ((System.Windows.Controls.Button)sender).Name.ToString().Trim();
            } 

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
            StatusLabel.Text = currentState;
        }

        public Rectangle Draw_Selection_Frame(BaseComponent selectedComponent)
        {
            Rectangle selectionFrame = new Rectangle
            {
                Width = selectedComponent.Width + 10,
                Height = selectedComponent.Height + 10,
                Fill = null,
                StrokeThickness = 2,
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection() { 4, 1 }
            };

            Canvas.SetLeft(selectionFrame, Canvas.GetLeft(selectedComponent) - 5);// - (int)(selectedComponent.Width / 4));
            Canvas.SetTop(selectionFrame, Canvas.GetTop(selectedComponent) - 5); //- (int)(selectedComponent.Height / 4));

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

        private void SimulationToggle_Click(object sender, RoutedEventArgs e)
        {
            foreach (FrameworkElement i in DrawingBoard.Children)
            {
                if (i.GetType().ToString() == "CircuitrySimulator.Classes.Power")
                    i.Tag = (bool)i.Tag == false ? true : false;
            }
        }
    }
}
