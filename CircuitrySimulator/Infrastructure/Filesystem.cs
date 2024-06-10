using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                List<SerializebleData> data = SerializeCircuitData();

                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;

                using (StreamWriter sw = new StreamWriter(currentFileName))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFileName == "" || currentFileName == null)
            {
                SaveAsButton_Click(sender, e);
            }
            else
            {
                List<SerializebleData> data = SerializeCircuitData();

                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;

                using (StreamWriter sw = new StreamWriter(currentFileName))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, data);
                }
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
                // Save document
                currentFileName = dialog.FileName;

                List<SerializebleData> data = JsonConvert.DeserializeObject<List<SerializebleData>>(File.ReadAllText(currentFileName));

                DeserializeCircuitData(data);
            }
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            DrawingBoard.Children.Clear();

            ClearState();
        }
    }
}
