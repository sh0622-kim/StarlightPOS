using StarlightPOS.ViewModels;
using Wpf.Ui.Controls;

namespace StarlightPOS.Views.Pages
{
    /// <summary>
    /// SalesHistoryPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SalesHistoryPage : INavigableView<SalesHistoryViewModel>, INavigationAware
    {
        public SalesHistoryViewModel ViewModel { get; }

        public SalesHistoryPage(SalesHistoryViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        public void OnNavigatedTo()
        {
            ViewModel.InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
