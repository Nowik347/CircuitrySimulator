using System.IO;
using System.Windows;
using System.Text.Json;

namespace CircuitrySimulator
{
    public partial class MainWindow : Window
    {
        string currentFileName;

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Circuit"; // Default file name
            dialog.DefaultExt = ".cir"; // Default file extension
            dialog.Filter = "Circuit schemes (.cir)|*.cir"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                currentFileName = dialog.FileName;
                SaveButton_Click(sender, e);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFileName == null)
            {
                SaveAsButton_Click(sender, e);
            }

            //    // Construct a SoapFormatter and use it
            //    // to serialize the data to the stream.
            //SoapFormatter formatter = new SoapFormatter();
            //    try
            //    {
            //        formatter.Serialize(fs, addresses);
            //    }
            //    catch (SerializationException e)
            //    {
            //        Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            //        throw;
            //    }
            //    finally
            //    {
            //        fs.Close();
            //    }

            //using (var stream = File.Open(currentFileName, FileMode.Create))
            //{
            //    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
            //    {
            //        writer.Write((object)DrawingBoard.Children);
            //        writer.Write(e);
            //    }
            //}

            //            //Create the stream to add object into it.  
            //            System.IO.Stream ms = File.OpenWrite(currentFileName);
            //            //Format the object as Binary  

            //            Inverter inverter = new Inverter(90);

            //            BinaryFormatter formatter = new BinaryFormatter();
            //            //It serialize the employee object  
            //#pragma warning disable SYSLIB0011 // Тип или член устарел
            //            //foreach (UIElement i in DrawingBoard.Children)
            //            formatter.Serialize(ms, inverter);
            //#pragma warning restore SYSLIB0011 // Тип или член устарел
            //            ms.Flush();
            //            ms.Close();
            //            ms.Dispose();

            //FileStream fs = File.Open(currentFileName, FileMode.Create);
            //XamlWriter.Save(DrawingBoard, fs);
            //fs.Close();

            StreamWriter writer = new StreamWriter(currentFileName);

            var options = new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
            };

            try
            {
                foreach (UIElement i in DrawingBoard.Children)
                {
                    var contentsToWriteToFile = JsonSerializer.Serialize(i, options);
                    writer.Write(contentsToWriteToFile);
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
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
                currentFileName = dialog.FileName;
                string jsonString = File.ReadAllText(currentFileName);
                var objects = JsonSerializer.Deserialize<UIElement>(jsonString)!;
                DrawingBoard.Children.Clear();
                DrawingBoard.Children.Add(objects);
            }         
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            DrawingBoard.Children.Clear();
        }
    }
}
