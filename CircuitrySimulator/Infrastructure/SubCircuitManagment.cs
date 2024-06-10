using CircuitrySimulator.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        public Canvas? LoadSubcircuitFromFileButton(SubCircuit sender, string filename = null)
        {
            if (filename == null)
            {
                // Configure save file dialog box
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.FileName = "Circuit"; // Default file name
                dialog.DefaultExt = ".cir"; // Default file extension
                dialog.Filter = "Circuit schemes (.cir)|*.cir"; // Filter files by extension

                // Show save file dialog box
                bool? result = dialog.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    List<SerializebleData> data = JsonConvert.DeserializeObject<List<SerializebleData>>(File.ReadAllText(dialog.FileName));

                    sender.circuitFileName = dialog.FileName;

                    return DeserializeSubCircuitData(data);
                }
            }
            else
            {
                List<SerializebleData> data = JsonConvert.DeserializeObject<List<SerializebleData>>(File.ReadAllText(filename));

                return DeserializeSubCircuitData(data);
            }

            return null;
        }

        public Canvas DeserializeSubCircuitData(List<SerializebleData> circuitData)
        {
            Canvas newCanvas = new Canvas();

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
                        object element_a = FindElement(item.data[0], newCanvas);

                        if (element_a == null)
                            break;

                        object element_b = FindElement(item.data[1], newCanvas);

                        if (element_b == null)
                            break;

                        PlaceWire(element_a, newCanvas, true);
                        PlaceWire(element_b, newCanvas, true);
                        break;
                    case "CircuitrySimulator.Classes.SubCircuit":
                        SubCircuit newSubcircuit = new SubCircuit(int.Parse(item.data[0]), item.data[2]);

                        totalComponentCount++;

                        newSubcircuit.Name = item.data[1];

                        Canvas.SetLeft(newSubcircuit, item.position.Value.X);
                        Canvas.SetTop(newSubcircuit, item.position.Value.Y);

                        newCanvas.Children.Add(newSubcircuit);
                        break;
                    default:
                        BaseComponent? newObject = Activator.CreateInstance(Type.GetType(item.typeName), new object[] { int.Parse(item.data[0]) }) as BaseComponent;

                        totalComponentCount++;

                        newObject.Name = item.data[1];

                        Canvas.SetLeft(newObject, item.position.Value.X);
                        Canvas.SetTop(newObject, item.position.Value.Y);

                        newCanvas.Children.Add(newObject);
                        break;
                }
            }

            return newCanvas;
        }
    }
}
