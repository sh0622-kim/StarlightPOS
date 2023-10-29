using CommunityToolkit.Mvvm.ComponentModel;
using StarlightPOS.Controls;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using StarlightPOS.Services;
using StarlightPOS.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui;

namespace StarlightPOS.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IOrderService _orderService;
        public Grid LeftGrid { get; set; }

        [ObservableProperty]
        private ObservableCollection<Table> _tableItems = new();

        public DashboardViewModel(INavigationService navigationService, IOrderService orderService)
        {
            _navigationService = navigationService;
            _orderService = orderService;
        }

        internal void Initialize()
        {
            LeftGrid.Children.Clear();
            LeftGrid.ColumnDefinitions.Clear();
            LeftGrid.RowDefinitions.Clear();

            for (int i = 0; i < DataManager.Setting.TableCols; i++)
            {
                LeftGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < DataManager.Setting.TableRows; i++)
            {
                LeftGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < DataManager.Tables.Count; i++)
            {
                var control = new TableControl
                {
                    DataContext = DataManager.Tables[i]
                };

                control.RootButton.Click += TableClick;

                LeftGrid.Children.Add(control);

                Grid.SetColumn(control, DataManager.Tables[i].Column);
                Grid.SetRow(control, DataManager.Tables[i].Row);
            }

            TableItems.Clear();
            List<Table> tables = new List<Table>(DataManager.Tables);
            var sortedTables = tables.OrderBy(table => table.CurrentOrder.Date).ToList();

            foreach (var table in sortedTables)
            {
                if (table.CurrentOrder.Items.Any())
                {
                    bool hasUndeliveredItems = table.CurrentOrder.Items.Any(item => !item.IsDelivered);

                    if (hasUndeliveredItems)
                    {
                        TableItems.Add(table);
                    }
                }
            }

        }

        private void TableClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _orderService.TableIndex = DataManager.Tables.ToList().FindIndex(x => x == ((FrameworkElement)sender).DataContext);
            _navigationService.Navigate(typeof(OrderPage));
        }
    }
}
