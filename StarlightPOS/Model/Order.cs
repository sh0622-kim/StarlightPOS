using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StarlightPOS.Model
{
    public class Order : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public ObservableCollection<OrderItem> Items { get; set; } = new ObservableCollection<OrderItem>();
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
