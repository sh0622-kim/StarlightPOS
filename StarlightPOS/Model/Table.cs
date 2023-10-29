using System;
using System.ComponentModel;

namespace StarlightPOS.Model
{
    public class Table : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Column { get; set; }
        public int Row { get; set; }
        public Order CurrentOrder { get; set; } = new Order();

        public event PropertyChangedEventHandler? PropertyChanged;
        public Table Copy()
        {
            return (Table)this.MemberwiseClone();
        }
    }
}
