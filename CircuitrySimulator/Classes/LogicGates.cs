using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;

namespace CircuitrySimulator.Classes
{
    class AND : BaseComponent
    {
        public AND(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/AND_edited.png", UriKind.Relative));
            Width = 60;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
            internalRotationAngle = rotationAngle;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input1 = new Line
            {
                Name = this.Name + "_input1",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 8,
                Y1 = Canvas.GetTop(this) + 25,
                Y2 = Canvas.GetTop(this) + 25,
            };

            Line input2 = new Line
            {
                Name = this.Name + "_input2",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 8,
                Y1 = Canvas.GetTop(this) + 45,
                Y2 = Canvas.GetTop(this) + 45,
            };

            Binding binding1 = new Binding
            {
                Source = input1,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding1);

            input1.Stroke = new SolidColorBrush(Colors.Black);

            Binding binding2 = new Binding
            {
                Source = input2,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding2);

            input2.Stroke = new SolidColorBrush(Colors.Black);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 52,
                X2 = Canvas.GetLeft(this) + 77,
                Y1 = Canvas.GetTop(this) + 35,
                Y2 = Canvas.GetTop(this) + 35,
            };

            input1.MouseLeftButtonUp += ChildrenOnClick;
            input2.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input1.MouseEnter += ChildrenMouseEnter;
            input2.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input1.MouseLeave += ChildrenMouseLeave;
            input2.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input1);
            IOLines.Add(input2);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input1);
            tempWindow.PlaceChildObject(input2);
            tempWindow.PlaceChildObject(output);
        }

        protected override void Simulate()
        {
            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() && IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }

    class OR : BaseComponent
    {
        public OR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/OR_edited.png", UriKind.Relative));
            Width = 60;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input1 = new Line
            {
                Name = this.Name + "_input1",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 15,
                Y1 = Canvas.GetTop(this) + 25,
                Y2 = Canvas.GetTop(this) + 25
            };

            Line input2 = new Line
            {
                Name = this.Name + "_input2",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 15,
                Y1 = Canvas.GetTop(this) + 45,
                Y2 = Canvas.GetTop(this) + 45,
            };

            Binding binding1 = new Binding
            {
                Source = input1,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding1);

            input1.Stroke = new SolidColorBrush(Colors.Black);

            Binding binding2 = new Binding
            {
                Source = input2,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding2);

            input2.Stroke = new SolidColorBrush(Colors.Black);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 53,
                X2 = Canvas.GetLeft(this) + 77,
                Y1 = Canvas.GetTop(this) + 35,
                Y2 = Canvas.GetTop(this) + 35,
            };

            input1.MouseLeftButtonUp += ChildrenOnClick;
            input2.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input1.MouseEnter += ChildrenMouseEnter;
            input2.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input1.MouseLeave += ChildrenMouseLeave;
            input2.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input1);
            IOLines.Add(input2);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input1);
            tempWindow.PlaceChildObject(input2);
            tempWindow.PlaceChildObject(output);
        }

        protected override void Simulate()
        {
            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() || IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }

    class NAND : BaseComponent
    {
        public NAND(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/NAND_edited.png", UriKind.Relative));
            Width = 70;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input1 = new Line
            {
                Name = this.Name + "_input1",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 8,
                Y1 = Canvas.GetTop(this) + 25,
                Y2 = Canvas.GetTop(this) + 25,
            };

            Line input2 = new Line
            {
                Name = this.Name + "_input2",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 8,
                Y1 = Canvas.GetTop(this) + 45,
                Y2 = Canvas.GetTop(this) + 45,
            };

            Binding binding1 = new Binding
            {
                Source = input1,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding1);

            input1.Stroke = new SolidColorBrush(Colors.Black);

            Binding binding2 = new Binding
            {
                Source = input2,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding2);

            input2.Stroke = new SolidColorBrush(Colors.Black);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 60,
                X2 = Canvas.GetLeft(this) + 85,
                Y1 = Canvas.GetTop(this) + 34,
                Y2 = Canvas.GetTop(this) + 34,
            };

            input1.MouseLeftButtonUp += ChildrenOnClick;
            input2.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input1.MouseEnter += ChildrenMouseEnter;
            input2.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input1.MouseLeave += ChildrenMouseLeave;
            input2.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input1);
            IOLines.Add(input2);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input1);
            tempWindow.PlaceChildObject(input2);
            tempWindow.PlaceChildObject(output);
        }

        protected override void Simulate()
        {
            if (IOLines[0].Stroke.ToString() == Colors.Black.ToString() && IOLines[1].Stroke.ToString() == Colors.Black.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = false;
            }
        }
    }

    class NOR : BaseComponent
    {
        public NOR(double rotationAngle)
        {
            Source = new BitmapImage(new Uri("../Images/Components/Edited/NOR_edited.png", UriKind.Relative));
            Width = 70;
            Height = 70;
            RenderTransform = new RotateTransform(rotationAngle);
            RenderTransformOrigin = new Point(0.5, 0.5);
            Focusable = true;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Line input1 = new Line
            {
                Name = this.Name + "_input1",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 15,
                Y1 = Canvas.GetTop(this) + 25,
                Y2 = Canvas.GetTop(this) + 25
            };

            Line input2 = new Line
            {
                Name = this.Name + "_input2",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) - 15,
                X2 = Canvas.GetLeft(this) + 15,
                Y1 = Canvas.GetTop(this) + 45,
                Y2 = Canvas.GetTop(this) + 45,
            };

            Binding binding1 = new Binding
            {
                Source = input1,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding1);

            input1.Stroke = new SolidColorBrush(Colors.Black);

            Binding binding2 = new Binding
            {
                Source = input2,
                Path = new PropertyPath("Tag"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
            };

            this.SetBinding(Image.TagProperty, binding2);

            input2.Stroke = new SolidColorBrush(Colors.Black);

            Line output = new Line
            {
                Name = this.Name + "_output",
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                X1 = Canvas.GetLeft(this) + 60,
                X2 = Canvas.GetLeft(this) + 85,
                Y1 = Canvas.GetTop(this) + 34,
                Y2 = Canvas.GetTop(this) + 34,
            };

            input1.MouseLeftButtonUp += ChildrenOnClick;
            input2.MouseLeftButtonUp += ChildrenOnClick;
            output.MouseLeftButtonUp += ChildrenOnClick;

            input1.MouseEnter += ChildrenMouseEnter;
            input2.MouseEnter += ChildrenMouseEnter;
            output.MouseEnter += ChildrenMouseEnter;

            input1.MouseLeave += ChildrenMouseLeave;
            input2.MouseLeave += ChildrenMouseLeave;
            output.MouseLeave += ChildrenMouseLeave;

            IOLines.Add(input1);
            IOLines.Add(input2);
            IOLines.Add(output);

            tempWindow.PlaceChildObject(input1);
            tempWindow.PlaceChildObject(input2);
            tempWindow.PlaceChildObject(output);
        }

        protected override void Simulate()
        {
            if (IOLines[0].Stroke.ToString() == Colors.Green.ToString() || IOLines[1].Stroke.ToString() == Colors.Green.ToString())
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Black);
                IOLines[2].Tag = true;
            }
            else
            {
                IOLines[2].Stroke = new SolidColorBrush(Colors.Green);
                IOLines[2].Tag = false;
            }
        }
    }
}
