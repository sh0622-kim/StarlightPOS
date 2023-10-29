using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using StarlightPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StarlightPOS.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        [ObservableProperty]
        private Table _table;

        [ObservableProperty]
        private ObservableCollection<Product> _products = DataManager.Products;

        [ObservableProperty]

        private ObservableCollection<OrderItem> _newOrders = new ObservableCollection<OrderItem>();


        private readonly IOrderService _orderService;
        public OrderViewModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        internal void Initialize()
        {
            Table = DataManager.Tables[_orderService.TableIndex];
            _newOrders.Clear();
        }

        [RelayCommand]
        private void OnPlus(ObservableCollection<Product> sender)
        {
            var item = sender.ToList().Find(x => x.IsSelected == true);

            var orderItem = _newOrders.ToList().Find(x => x.Product.Id == item.Id);

            if (orderItem == null)
            {
                var obj = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    Product = item,
                    Quantity = 1
                };

                obj.TotalAmount = obj.Quantity * obj.Product.Price;
                _newOrders.Add(obj);
            }
            else
            {
                orderItem.Quantity++;
                orderItem.TotalAmount = orderItem.Quantity * orderItem.Product.Price;
            }
        }

        [RelayCommand]
        private void OnMinus(ObservableCollection<Product> sender)
        {
            var item = sender.ToList().Find(x => x.IsSelected == true);

            var orderItem = _newOrders.ToList().Find(x => x.Product.Id == item.Id);

            if (orderItem == null)
            {
                var obj = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    Product = item,
                    Quantity = -1
                };

                obj.TotalAmount = obj.Quantity * obj.Product.Price;
                _newOrders.Add(obj);
            }
            else
            {
                orderItem.Quantity--;
                orderItem.TotalAmount = orderItem.Quantity * orderItem.Product.Price;
            }
        }

        [RelayCommand]
        private async void OnOrder(string parameter)
        {
            for (int i = 0; i < Products.Count; i++)
            {
                var all = _newOrders.ToList().FindAll(x => x.Product.Id == Products[i].Id);
                var q = 0;
                for (int j = 0; j < all.Count; j++)
                {
                    q = q + all[j].Quantity;
                }

                var product = Products[i];
                var remainingStock = product.Stock - q;

                if (remainingStock < 0)
                {
                    var messageBox = new Wpf.Ui.Controls.MessageBox
                    {
                        Title = "재고 부족 알림",
                        Content = $"{product.Name}의 현재 재고는 {product.Stock}개입니다. {q}개를 주문할 수 없습니다.",
                    };

                    await messageBox.ShowDialogAsync();
                    return;
                }
            }

            for (int i = 0; i < Products.Count; i++)
            {
                var all = _newOrders.ToList().FindAll(x => x.Product.Id == Products[i].Id);
                var q = 0;
                for (int j = 0; j < all.Count; j++)
                {
                    q = q + all[j].Quantity;
                }

                var product = Products[i];
                product.Stock = product.Stock - q;
            }

            for (int i = 0; i < _newOrders.Count; i++)
            {
                Table.CurrentOrder.Items.Add(_newOrders[i]);
            }

            decimal total = 0;

            for (int i = 0; i < Table.CurrentOrder.Items.Count; i++)
            {
                total += Table.CurrentOrder.Items[i].TotalAmount;
            }
            Table.CurrentOrder.TotalAmount = total;
            Table.CurrentOrder.Date = DateTime.Now;
            Table.CurrentOrder.Id = Guid.NewGuid();
            _newOrders.Clear();

            DataManager.SaveData("settings\\products.json", DataManager.Products);
            DataManager.SaveData("settings\\tables.json", DataManager.Tables);
        }

        [RelayCommand]
        private void OnCancel(string parameter)
        {
            _newOrders.Clear();
        }

        [RelayCommand]
        private async void OnInvoice(string parameter)
        {
            for (int i = 0; i < Table.CurrentOrder.Items.Count; i++)
            {
                if (Table.CurrentOrder.Items[i].IsDelivered == false)
                {
                    var messageBox = new Wpf.Ui.Controls.MessageBox
                    {
                        Title = "알림",
                        Content = "서빙 되지 않은 메뉴가 있습니다. 완료 처리 후 계산서 발행이 가능합니다."
                    };

                    await messageBox.ShowDialogAsync();
                    return;
                }
            }

            string content
            = $"계산서" + Environment.NewLine +
              $"----------------------------" + Environment.NewLine +
              $"테이블 : {Table.Name}" + Environment.NewLine +
              $"결제 시간 : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}" + Environment.NewLine +
              $"총 결제 금액 : {Table.CurrentOrder.TotalAmount.ToString("N0")} 원" + Environment.NewLine +
               $"----------------------------" + Environment.NewLine;

            for (int i = 0; i < Table.CurrentOrder.Items.Count; i++)
            {
                if (i < 10)
                {
                    content += $"{Table.CurrentOrder.Items[i].Product.Name}\t{Table.CurrentOrder.Items[i].Quantity}\t{Table.CurrentOrder.Items[i].TotalAmount}\t" + Environment.NewLine;

                }
                else
                {
                    content += "이하 절삭...";
                    break;
                }
            }
            {
                var messageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "계산서",
                    Content = content,
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "계산서 발행",
                    CloseButtonText = "취소"
                };

                var result = await messageBox.ShowDialogAsync();

                if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
                {
                    var h = Table.Copy();
                    DataManager.SalesHistory.Add(h);
                    DataManager.SaveData("settings\\salesHistory.json", DataManager.SalesHistory);
                    DataManager.SalesHistory = DataManager.LoadData<ObservableCollection<Table>>("settings\\salesHistory.json");
                    Table.Id = Guid.NewGuid();
                    Table.CurrentOrder.Id = Guid.NewGuid();
                    Table.CurrentOrder.Date = DateTime.Now;
                    Table.CurrentOrder.Items.Clear();
                    Table.CurrentOrder.TotalAmount = 0;
                    DataManager.SaveData("settings\\tables.json", DataManager.Tables);
                }
            }
        }
    }
}
