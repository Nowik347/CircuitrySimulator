using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CircuitrySimulator.Classes
{
    abstract public partial class BaseComponent : Image
    {
        public class PinConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                bool result = false;

                foreach (var item in values)
                {
                    if (result == true)
                        break;

                    result = (bool)item;
                }

                return result;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                return (object[])value;
            }
        }

        public Rectangle? selectionFrame;

        public List<UIElement> childrenElements = new List<UIElement>();
        public List<Line> IOLines = new List<Line>();
        public double internalRotationAngle;

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

            ((MainWindow)Application.Current.MainWindow).currentSelectedObject = this;

            selectionFrame = ((MainWindow)Application.Current.MainWindow).Draw_Selection_Frame(this);

            ((MainWindow)Application.Current.MainWindow).currentState = this.Name;

            ((MainWindow)Application.Current.MainWindow).UpdateStatusLabel();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            ((MainWindow)Application.Current.MainWindow).currentSelectedObject = null;

            ((MainWindow)Application.Current.MainWindow).Remove_Selection_Frame(selectionFrame);

            selectionFrame = null;
        }

        public void DeleteChildren()
        {
            foreach (UIElement i in childrenElements)
                ((MainWindow)Application.Current.MainWindow).DeleteChildObject(i);

            foreach (UIElement i in IOLines)
                ((MainWindow)Application.Current.MainWindow).DeleteChildObject(i);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            switch (((MainWindow)Application.Current.MainWindow).currentState)
            {
                case "Deletion":
                    DeleteChildren();
                    ((MainWindow)Application.Current.MainWindow).DeleteObject(this);
                    break;
                case "Wiring":
                case "Placement":
                    break;
                default:
                    Keyboard.Focus(this);
                    break;
            }
        }

        protected virtual void Simulate() { }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (this.IsInitialized && IOLines.Count != 0)
                Simulate();
        }
    }
}
