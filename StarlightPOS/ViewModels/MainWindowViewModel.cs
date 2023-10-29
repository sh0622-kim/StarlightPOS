using CommunityToolkit.Mvvm.ComponentModel;
using StarlightPOS.Managers;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace StarlightPOS.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = new();

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = $"StarlightPOS - {DataManager.Setting.Name}";

            NavigationItems = new ObservableCollection<object>
            {
                new NavigationViewItem()
                {
                    Content = "테이블",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(Views.Pages.DashboardPage)
                }
            };

            NavigationFooter = new ObservableCollection<object>
            {
                 new NavigationViewItem()
                {
                    Content = "상품관리",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.VehicleTruckBag24 },
                    TargetPageType = typeof(Views.Pages.ProductPage)
                },
                new NavigationViewItem()
                {
                    Content = "정산",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.DocumentBulletList24 },
                    TargetPageType = typeof(Views.Pages.SalesHistoryPage)
                },
                new NavigationViewItem()
                {
                    Content = "설정",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(Views.Pages.SettingsPage)
                }
            };

            _isInitialized = true;
        }
    }
}