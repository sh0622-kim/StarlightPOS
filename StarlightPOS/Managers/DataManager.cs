using StarlightPOS.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace StarlightPOS.Managers
{
    internal class DataManager
    {
        public static Setting Setting { get; set; } = new Setting();
        public static ObservableCollection<Table> Tables { get; set; } = new ObservableCollection<Table>();

        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public static ObservableCollection<Table> SalesHistory { get; set; } = new ObservableCollection<Table>();

        public static void SaveData<T>(string fileName, T data)
        {
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(fileName));

            if (di.Exists == false)
            {
                di.Create();
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Default
            });
            File.WriteAllText(fileName, json, Encoding.Unicode);
        }

        public static T? LoadData<T>(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (fi.Exists == false)
            {
                T obj = Activator.CreateInstance<T>();
                SaveData<T>(fileName, obj);
                return obj;
            }
            else
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
