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

            if (currentSelectedObject != null)
            {
                ClosePropertiesPanel();

                Remove_Selection_Frame(currentSelectedObject.selectionFrame);

                currentSelectedObject.selectionFrame = null;

                currentSelectedObject = null;
            }

            SelectionToggleButton.IsChecked = true;

            DeleteToggleButton.IsChecked = false;
            WiringToggleButton.IsChecked = false;
            drawPreviewImage = false;
            newElementName = null;

            UpdateStatusLabel();
        }

        public void OpenPropertiesPanel()
        {
            PropertiesPanel.Visibility = Visibility.Visible;

            switch (currentSelectedObject.GetType().ToString())
            {
                case "CircuitrySimulator.Classes.SubCircuit":
                    SubCircuit subCircuit = currentSelectedObject as SubCircuit;

                    NameLabel.Visibility = Visibility.Visible;
                    NameTextbox.Visibility = Visibility.Visible;

                    NameTextbox.Text = currentSelectedObject.Name;

                    SizeLabel.Visibility = Visibility.Visible;
                    SizeTextbox.Visibility = Visibility.Visible;

                    SizeTextbox.Text = subCircuit.Width.ToString();

                    LoadedCircuitLabel.Visibility = Visibility.Visible;
                    LoadedCircuitTextBlock.Visibility = Visibility.Visible;

                    LoadedCircuitTextBlock.Text = subCircuit.circuitFileName;

                    break;
                case "CircuitrySimulator.Classes.SubCircuitPin":
                    SubCircuitPin subCircuitPin = currentSelectedObject as SubCircuitPin;

                    NameLabel.Visibility = Visibility.Visible;
                    NameTextbox.Visibility = Visibility.Visible;

                    NameTextbox.Text = currentSelectedObject.Name;

                    PinLabelLabel.Visibility = Visibility.Visible;
                    PinLabelTextbox.Visibility = Visibility.Visible;

                    PinLabelTextbox.Text = subCircuitPin.pinLabel;
                    break;
                default:
                    NameLabel.Visibility = Visibility.Visible;
                    NameTextbox.Visibility = Visibility.Visible;

                    NameTextbox.Text = currentSelectedObject.Name;
                    break;
            }
        }

        public void ClosePropertiesPanel()
        {
            PropertiesPanel.Visibility = Visibility.Collapsed;

            NameLabel.Visibility = Visibility.Collapsed;
            NameTextbox.Visibility = Visibility.Collapsed;

            SizeLabel.Visibility = Visibility.Collapsed;
            SizeTextbox.Visibility = Visibility.Collapsed;

            LoadedCircuitLabel.Visibility = Visibility.Collapsed;
            LoadedCircuitTextBlock.Visibility = Visibility.Collapsed;

            PinLabelLabel.Visibility = Visibility.Collapsed;
            PinLabelTextbox.Visibility = Visibility.Collapsed;
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
                //if (i.GetType().ToString() == "CircuitrySimulator.Classes.Power")
                //    i.Tag = (bool)i.Tag == false ? true : false;

                if (i.GetType().ToString() == "CircuitrySimulator.Classes.Power")
                {
                    Power power = i as Power;

                    power.Update();
                }
            }
        }

        private void NameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentSelectedObject != null)
                currentSelectedObject.Name = NameTextbox.Text;
        }

        private void SizeTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentSelectedObject != null)
                try
                {
                    if (int.Parse(SizeTextbox.Text) > 0)
                        currentSelectedObject.Name = NameTextbox.Text;
                    else
                        SizeTextbox.Text = "1";
                }
                catch (System.FormatException)
                {
                    SizeTextbox.Text = "1";
                }
        }

        private void PinLabelTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
