using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Ink;

namespace CircuitrySimulator.Classes
{
    public class SubCircuit : BaseComponent
    {
        private Canvas? internalCanvas;
        private List<SubCircuitPin> subCircuitPins_left = new List<SubCircuitPin>(), subCircuitPins_right = new List<SubCircuitPin>();

        public SubCircuit(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/subcircuit_edited.png", UriKind.Relative));
            Width = 50;
            Height = 100;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            internalCanvas = ((MainWindow)Application.Current.MainWindow).LoadSubcircuitFromFileButton();
        }

        protected override void OnInitialized(EventArgs e)
        {
            int left_pins_count = 0, right_pins_count = 0;

            foreach (var item in internalCanvas.Children)
                if (item.GetType().ToString() == "CircuitrySimulator.Classes.SubCircuitPin")
                {
                    SubCircuitPin subCircuitPin = item as SubCircuitPin;

                    if (subCircuitPin.internalRotationAngle <= 90 || subCircuitPin.internalRotationAngle == 360)
                    {
                        left_pins_count++;
                        subCircuitPins_left.Add(subCircuitPin);
                    }
                    else
                    {
                        right_pins_count++;
                        subCircuitPins_right.Add(subCircuitPin);
                    }
                }

            IOLines = CreatePins(left_pins_count, right_pins_count);

            foreach (var item in IOLines)
            {
                ((MainWindow)Application.Current.MainWindow).PlaceChildObject(item);
                item.Stroke = new SolidColorBrush(Colors.Black);
            }
        }

        protected override void Simulate()
        {
            base.Simulate();

            for (int i = 0; i < subCircuitPins_left.Count; i++)
            {
                subCircuitPins_left[i].IOLines[0].Stroke = IOLines[i].Stroke;
                subCircuitPins_left[i].IOLines[0].Tag = IOLines[i].Tag;
            }

            for (int j = 0; j < subCircuitPins_right.Count; j++)
            {
                IOLines[j + subCircuitPins_left.Count].Stroke = subCircuitPins_right[j].IOLines[0].Stroke;
                IOLines[j + subCircuitPins_left.Count].Tag = subCircuitPins_right[j].IOLines[0].Tag;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
        }
    }

    public class SubCircuitPin : BaseComponent
    {
        public SubCircuitPin(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/subcircuitpin_edited.png", UriKind.Relative));
            Width = 30;
            Height = 30;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
            Tag = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            IOLines = CreatePins(0, 1);

            IOLines[0].Stroke = new SolidColorBrush(Colors.Black);
            IOLines[0].Tag = false;

            base.OnInitialized(e);
        }
    }
}
