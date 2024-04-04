using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace CircuitrySimulator.Classes
{
    abstract public class BaseComponent : Image
    {
        Rectangle? selectionFrame;
        List<UIElement> childrenElements = new List<UIElement>();

        protected override void OnInitialized(EventArgs e)
        {
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

            Canvas.SetLeft(componentLabel, Canvas.GetLeft(this) + 10);
            Canvas.SetTop(componentLabel, Canvas.GetTop(this) + 100);

            var tempWindow = (MainWindow)Application.Current.MainWindow;

            tempWindow.PlaceChildObject(componentLabel);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            double x = Canvas.GetLeft(this);
            double y = Canvas.GetTop(this);

            var tempWindow = (MainWindow)Application.Current.MainWindow;

            tempWindow.currentSelectedObject = this;

            selectionFrame = tempWindow.Draw_Selection_Frame(x, y);

            tempWindow.currentState = this.Name;

            tempWindow.UpdateStatusLabel();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            var tempWindow = (MainWindow)Application.Current.MainWindow;

            tempWindow.currentSelectedObject = null;

            tempWindow.Remove_Selection_Frame(selectionFrame);

            selectionFrame = null;
        }

        public void DeleteChildren()
        {
            var tempWindow = (MainWindow)Application.Current.MainWindow;

            foreach (UIElement i in childrenElements)
                tempWindow.DeleteChildObject(i);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var tempWindow = (MainWindow)Application.Current.MainWindow;

            switch(tempWindow.currentState)
            {
                case "Deletion":
                    DeleteChildren();
                    tempWindow.DeleteObject(this);
                    break;
                case "Placement":
                    break;
                default:
                    Keyboard.Focus(this);
                    break;
            }
        }
    }
}
