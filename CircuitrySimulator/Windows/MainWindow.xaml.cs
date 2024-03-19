using System.Windows;
using System.Windows.Controls;

namespace CircuitrySimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum State { Selection, Transistor }
        readonly string []StatusLabelStates = { "", "Транзистор" };
        readonly string []PreviewImagesSources = { "", "../Images/transistor.png" };

        private int _rotationAngle;

        public int rotationAngle 
        {
            get { return _rotationAngle; }

            set { _rotationAngle = _rotationAngle == 360 ? 90 : value; }
        }

        State currentState;

        public MainWindow()
        {
            InitializeComponent();

            currentState = State.Selection;
            StatusLabel.Content = StatusLabelStates[(int)currentState];
        }

        private void SetState_FromButton(object sender, RoutedEventArgs e)
        {
            switch(((Button)sender).Name)
            {
                case "TransistorButton":
                    currentState = currentState == State.Selection ? State.Transistor : State.Selection;
                    break;
                default:
                    currentState = State.Selection;
                    break;
            }

            StatusLabel.Content = StatusLabelStates[(int)currentState];
        }
    }
}
