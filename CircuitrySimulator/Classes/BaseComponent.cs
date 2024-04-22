using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircuitrySimulator.Classes
{
    abstract public class BaseComponent : Image
    {
        Rectangle? selectionFrame;

        public static readonly DependencyProperty EmptyProperty = DependencyProperty.Register(name: "EmptyProperty", propertyType: typeof(Brush), ownerType: typeof(BaseComponent), typeMetadata: new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Black)));

        public Brush StubEmptyProperty
        {
            get => (Brush)GetValue(EmptyProperty);
            set => SetValue(EmptyProperty, value);
        }

        public List<UIElement> childrenElements = new List<UIElement>();
        public List<Line> IOLines = new List<Line>();
        public double internalRotationAngle;
        public int labelCorrectionX, labelCorrectionY;

        readonly public MainWindow tempWindow = (MainWindow)Application.Current.MainWindow;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Binding binding = new Binding
            {
                Source = this,
                Path = new PropertyPath("Name"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            TextBox componentLabel = new TextBox();
            childrenElements.Add(componentLabel);

            componentLabel.SetBinding(TextBox.TextProperty, binding);

            componentLabel.FontSize = 14; 

            Canvas.SetLeft(componentLabel, Canvas.GetLeft(this) + labelCorrectionX);
            Canvas.SetTop(componentLabel, Canvas.GetTop(this) + this.Height + labelCorrectionY);

            tempWindow.PlaceChildObject(componentLabel);

            this.TargetUpdated += MyTargetUpdated;
            this.SourceUpdated += MyTargetUpdated;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            foreach (Line line in IOLines) 
            {
                line.StrokeThickness = 5;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            foreach (Line line in IOLines)
            {
                line.StrokeThickness = 1;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            double x = Canvas.GetLeft(this);
            double y = Canvas.GetTop(this);

            tempWindow.currentSelectedObject = this;

            selectionFrame = tempWindow.Draw_Selection_Frame(x, y);

            tempWindow.currentState = this.Name;

            tempWindow.UpdateStatusLabel();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            tempWindow.currentSelectedObject = null;

            tempWindow.Remove_Selection_Frame(selectionFrame);

            selectionFrame = null;
        }

        public void DeleteChildren()
        {
            foreach (UIElement i in childrenElements)
                tempWindow.DeleteChildObject(i);

            foreach (UIElement i in IOLines)
                tempWindow.DeleteChildObject(i);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            switch(tempWindow.currentState)
            {
                case "Deletion":
                    DeleteChildren();
                    tempWindow.DeleteObject(this);
                    break;
                case "Wiring":
                case "Placement":
                    break;
                default:
                    Keyboard.Focus(this);
                    break;
            }
        }

        private void MyTargetUpdated(object sender, DataTransferEventArgs e) 
        {
            Simulate();
        }

        protected virtual void Simulate() { }

        public void ChildrenOnClick(object sender, MouseButtonEventArgs e) 
        {
            //Point newPoint = new Point((line.X1 + line.X2) / 2, (line.Y1 + line.Y2) / 2);
            tempWindow.PlaceWire(sender as Line);
        }

        public void ChildrenMouseEnter(object sender, MouseEventArgs e)
        {
            Line line = sender as Line;
            line.StrokeThickness = 5;
        }

        public void ChildrenMouseLeave(object sender, MouseEventArgs e)
        {
            Line line = sender as Line;
            line.StrokeThickness = 1;
        }
    }
}
