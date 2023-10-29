using System;
using System.ComponentModel;

namespace StarlightPOS.Model
{
    public class OrderItem : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public bool IsDelivered { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
