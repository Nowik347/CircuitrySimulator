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
                    if ((bool)item == true)
                    {
                        result = (bool)item;
                        break;
                    }
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

        private List<string> previousState = new List<string>();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Canvas parentCanvas = this.Parent as Canvas;

            foreach (var item in IOLines)
                parentCanvas.Children.Add(item);
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
                    if (((MainWindow)Application.Current.MainWindow).currentSelectedObject != null)
                    {
                        ((MainWindow)Application.Current.MainWindow).Remove_Selection_Frame(((MainWindow)Application.Current.MainWindow).currentSelectedObject.selectionFrame);

                        ((MainWindow)Application.Current.MainWindow).currentSelectedObject.selectionFrame = null;
                    }

                    ((MainWindow)Application.Current.MainWindow).currentSelectedObject = this;

                    ((MainWindow)Application.Current.MainWindow).OpenPropertiesPanel();

                    selectionFrame = ((MainWindow)Application.Current.MainWindow).Draw_Selection_Frame(this);

                    ((MainWindow)Application.Current.MainWindow).currentState = this.Name;

                    ((MainWindow)Application.Current.MainWindow).UpdateStatusLabel();
                    break;
            }
        }

        protected virtual void Simulate() { }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (this.IsInitialized && IOLines.Count != 0)                //Simulate();
            {
                if (previousState.Count == 0)
                {
                    foreach (Line line in IOLines)
                        previousState.Add(line.Stroke.ToString());

                    Simulate();
                }
                else
                {
                    for (int i = 0; i < IOLines.Count; i++)
                        if (IOLines[i].Stroke.ToString() != previousState[i])
                        {
                            for (int j = 0; j < IOLines.Count; j++)
                                previousState[j] = IOLines[j].Stroke.ToString();

                            Simulate();
                            break;
                        }

                    //if (IOLines.Count >= 3)
                    //{
                    //    if (IOLines[0].Stroke.ToString() != previousState[0] || IOLines[1].Stroke.ToString() != previousState[1])
                    //    {
                    //        for (int i = 0; i < IOLines.Count; i++)
                    //            previousState[i] = IOLines[i].Stroke.ToString();

                            //        Simulate();
                            //    }
                            //}
                            //else
                            //{
                            //    if (IOLines[0].Stroke.ToString() != previousState[0])
                            //    {
                            //        for (int i = 0; i < IOLines.Count; i++)
                            //            previousState[i] = IOLines[i].Stroke.ToString();

                            //        Simulate();
                            //    }
                            //}
                }
            }

        }
    }
}
