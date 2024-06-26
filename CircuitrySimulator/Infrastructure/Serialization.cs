﻿using CircuitrySimulator.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CircuitrySimulator
{
    public class SerializebleData
    { 
        public string? typeName;
        public Point? position;
        public List<string> data = new List<string>();

        public SerializebleData() { }
    }

    public partial class MainWindow : Window
    {
        private List<SerializebleData> SerializeCircuitData()
        {
            List<SerializebleData> circuitData = new List<SerializebleData>();
            
            if (currentSelectedObject != null)
            {
                Remove_Selection_Frame(currentSelectedObject.selectionFrame);

                currentSelectedObject.selectionFrame = null;

                currentSelectedObject = null;
            }

            foreach (var item in DrawingBoard.Children)
            {
                switch (item.GetType().ToString())
                {
                    case "System.Windows.Controls.TextBox":
                        TextBox textBox = item as TextBox;

                        SerializebleData textBoxData = new SerializebleData();

                        textBoxData.typeName = item.GetType().ToString();
                        textBoxData.position = new Point(Canvas.GetLeft(textBox), Canvas.GetTop(textBox));
                        textBoxData.data.Add(textBox.Text);

                        circuitData.Add(textBoxData);
                        break;
                    case "System.Windows.Shapes.Line":
                    case "System.Windows.Shapes.Ellipse":
                        break;
                    case "CircuitrySimulator.Classes.SubCircuit":
                        SubCircuit subcircuit = item as SubCircuit;

                        SerializebleData subcircuitData = new SerializebleData();

                        subcircuitData.typeName = item.GetType().ToString();
                        subcircuitData.position = new Point(Canvas.GetLeft(subcircuit), Canvas.GetTop(subcircuit));
                        subcircuitData.data.Add(subcircuit.internalRotationAngle.ToString());
                        subcircuitData.data.Add(subcircuit.Name);
                        subcircuitData.data.Add(subcircuit.circuitFileName);

                        circuitData.Add(subcircuitData);

                        break;
                    case "System.Windows.Shapes.Polyline":
                        Polyline polyline = item as Polyline;

                        SerializebleData polylineData = new SerializebleData();

                        polylineData.typeName = item.GetType().ToString();

                        if (polyline.Name.Contains('Ш'))
                        {
                            polylineData.data.Add(polyline.Name.Split('Ш')[0]);
                            polylineData.data.Add(polyline.Name.Split('Ш')[1]);
                        }
                        else if (polyline.Name.Contains('П'))
                        {
                            polylineData.data.Add(polyline.Name.Split('П')[0]);
                            polylineData.data.Add(polyline.Name.Split('П')[1]);
                        }
                        else
                        {
                            polylineData.data.Add(polyline.Name.Split('Ф')[0]);
                            polylineData.data.Add(polyline.Name.Split('Ф')[1]);
                        }

                        circuitData.Add(polylineData);
                        break;
                    default:
                        BaseComponent component = item as BaseComponent;

                        SerializebleData componentData = new SerializebleData();

                        componentData.typeName = item.GetType().ToString();
                        componentData.position = new Point(Canvas.GetLeft(component), Canvas.GetTop(component));
                        componentData.data.Add(component.internalRotationAngle.ToString());
                        componentData.data.Add(component.Name);

                        circuitData.Add(componentData);
                        break;
                }
            }

            return circuitData;
        }

        private void DeserializeCircuitData(List<SerializebleData> circuitData) 
        {
            DrawingBoard.Children.Clear();

            foreach (var item in circuitData)
            {
                switch (item.typeName)
                {
                    case "System.Windows.Controls.TextBox":
                        //TextBox textBox = item as TextBox;

                        //SerializebleData textBoxData = new SerializebleData();

                        //textBoxData.typeName = item.GetType().ToString();
                        //textBoxData.position = new Point(Canvas.GetLeft(textBox), Canvas.GetTop(textBox));
                        //textBoxData.data.Add(textBox.Text);

                        //circuitData.Add(textBoxData);
                        break;
                    case "System.Windows.Shapes.Line":
                        break;
                    case "System.Windows.Shapes.Polyline":
                        object element_a = FindElement(item.data[0], DrawingBoard);

                        if (element_a == null)
                            break;

                        object element_b = FindElement(item.data[1], DrawingBoard);

                        if (element_b == null)
                            break;

                        PlaceWire(element_a, DrawingBoard);
                        PlaceWire(element_b, DrawingBoard);
                        break;
                    case "CircuitrySimulator.Classes.SubCircuit":
                        SubCircuit newSubcircuit = new SubCircuit(int.Parse(item.data[0]), item.data[2]);

                        totalComponentCount++;

                        newSubcircuit.Name = item.data[1];

                        Canvas.SetLeft(newSubcircuit, item.position.Value.X);
                        Canvas.SetTop(newSubcircuit, item.position.Value.Y);

                        DrawingBoard.Children.Add(newSubcircuit);
                        break;
                    default:
                        BaseComponent? newObject = Activator.CreateInstance(Type.GetType(item.typeName), new object[] { int.Parse(item.data[0]) }) as BaseComponent;

                        totalComponentCount++;

                        newObject.Name = item.data[1];

                        Canvas.SetLeft(newObject, item.position.Value.X);
                        Canvas.SetTop(newObject, item.position.Value.Y);

                        DrawingBoard.Children.Add(newObject);
                        break;
                }
            }
        }

        private FrameworkElement FindElement(string name, Canvas canvas)
        {
            foreach (FrameworkElement item in canvas.Children)
            {
                if (item.Name == name)
                    return item;
            }

            return null;
        }
    }
}
