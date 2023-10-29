using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace StarlightPOS.Model
{
    public class Product : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Stock { get; set; } = 0;

        [JsonIgnore]
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
