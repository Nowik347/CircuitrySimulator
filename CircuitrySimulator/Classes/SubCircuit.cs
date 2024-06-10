using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace CircuitrySimulator.Classes
{
    public class SubCircuit : BaseComponent
    {
        private Canvas? internalCanvas;
        private List<SubCircuitPin> subCircuitPins_left = new List<SubCircuitPin>(), subCircuitPins_right = new List<SubCircuitPin>();
        public string? circuitFileName;

        public SubCircuit(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/subcircuit_edited.png", UriKind.Relative));
            Width = 70;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            Tag = false;
            internalRotationAngle = rotationAngle;
            internalCanvas = ((MainWindow)Application.Current.MainWindow).LoadSubcircuitFromFileButton(this);
        }

        public SubCircuit(double rotationAngle, string filename)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/subcircuit_edited.png", UriKind.Relative));
            Width = 70;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            Tag = false;
            internalRotationAngle = rotationAngle;
            internalCanvas = ((MainWindow)Application.Current.MainWindow).LoadSubcircuitFromFileButton(this, filename);
            circuitFileName = filename;
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

            Canvas parentCanvas = this.Parent as Canvas;

            foreach (var item in IOLines)
            {
                parentCanvas.Children.Add(item);
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

        //private List<Line> CreateSubCircuitPins()
        //{
        //    double currentWidth = 0, currentHeight = 0, horizontalSpacing = (this.Width / numberOfInputs) / 2, verticalSpacing = (this.Height / numberOfInputs) / 2;

        //    MultiBinding pinBindings = new MultiBinding();

        //    for (int i = numberOfInputs; i > 0; i--)
        //    {
        //        Line input = new Line
        //        {
        //            Name = this.Name + "_input" + i,
        //            StrokeThickness = 1,
        //            Tag = false,
        //        };

        //        switch (internalRotationAngle)
        //        {
        //            case 90:
        //                input.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
        //                input.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
        //                input.Y1 = Canvas.GetTop(this) + 2;
        //                input.Y2 = Canvas.GetTop(this) - 23;
        //                currentWidth += horizontalSpacing * 2;
        //                break;
        //            case 180:
        //                input.X1 = Canvas.GetLeft(this) + this.Width - 2;
        //                input.X2 = Canvas.GetLeft(this) + this.Width + 23;
        //                input.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
        //                input.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
        //                currentHeight += verticalSpacing * 2;
        //                break;
        //            case 270:
        //                input.X1 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
        //                input.X2 = Canvas.GetLeft(this) + currentWidth + horizontalSpacing;
        //                input.Y1 = Canvas.GetTop(this) + this.Height - 2;
        //                input.Y2 = Canvas.GetTop(this) + this.Height + 23;
        //                currentWidth += horizontalSpacing * 2;
        //                break;
        //            default:
        //                input.X1 = Canvas.GetLeft(this) + 2;
        //                input.X2 = Canvas.GetLeft(this) - 23;
        //                input.Y1 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
        //                input.Y2 = Canvas.GetTop(this) + currentHeight + verticalSpacing;
        //                currentHeight += verticalSpacing * 2;
        //                break;
        //        }

        //        Binding binding = new Binding
        //        {
        //            Source = input,
        //            Path = new PropertyPath("Tag"),
        //            Mode = BindingMode.TwoWay,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            NotifyOnSourceUpdated = true,
        //            NotifyOnTargetUpdated = true,
        //        };

        //        pinBindings.Bindings.Add(binding);

        //        input.Stroke = new SolidColorBrush(Colors.Black);

        //        input.MouseLeftButtonUp += ChildrenOnClick;

        //        input.MouseEnter += ChildrenMouseEnter;

        //        input.MouseLeave += ChildrenMouseLeave;

        //        newIO.Add(input);
        //    }

        //    pinBindings.Converter = new PinConverter();

        //    this.SetBinding(Image.TagProperty, pinBindings);

        //    return newIO;
        //}

        //private void UpdateSubCircuitPins() 
        //{
        
        //}

        //public void Resize(int newSize)
        //{

        //}
    }

    public class SubCircuitPin : BaseComponent
    {
        public string? pinLabel;

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
