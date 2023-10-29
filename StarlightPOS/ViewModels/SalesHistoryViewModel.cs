using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace StarlightPOS.ViewModels
{
    public partial class SalesHistoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Table> _tableItems = new ObservableCollection<Table>();

        [ObservableProperty]
        private decimal _totalAmount = 0;

        private List<string> _csv = new List<string>();

        public SalesHistoryViewModel()
        {

        }

        public void InitializeViewModel()
        {
            _csv.Clear();
            _csv.Add("테이블 생성 ID, 상품 주문 ID, 생성 시간, 테이블명, 상품명, 주문 수량, 매출");
            TableItems.Clear();
            TotalAmount = 0;
            var history = DataManager.SalesHistory;
            for (int i = 0; i < history.Count; i++)
            {
                for (int j = 0; j < history[i].CurrentOrder.Items.Count; j++)
                {
                    Table temp = new Table
                    {
                        Id = history[i].Id,
                        Name = history[i].Name
                    };
                    temp.CurrentOrder.Id = history[i].CurrentOrder.Id;
                    temp.CurrentOrder.Date = history[i].CurrentOrder.Date;
                    temp.CurrentOrder.TotalAmount = history[i].CurrentOrder.Items[j].TotalAmount;
                    temp.CurrentOrder.Items.Add(history[i].CurrentOrder.Items[j]);

                    TableItems.Add(temp);

                    TotalAmount = TotalAmount + temp.CurrentOrder.TotalAmount;

                    _csv.Add($"{history[i].Id}, {temp.CurrentOrder.Id}, {temp.CurrentOrder.Date}, {history[i].Name}, {temp.CurrentOrder.Items[0].Product.Name}, {temp.CurrentOrder.Items[0].Quantity}, {temp.CurrentOrder.Items[0].TotalAmount}");

                }
            }
        }

        [RelayCommand]
        private void OnSave(string parameter)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() != true)
            {
                return;
            }
            File.WriteAllLines(saveFileDialog.FileName, _csv, Encoding.UTF8);
        }
    }
}
