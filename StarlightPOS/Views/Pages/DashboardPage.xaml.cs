using StarlightPOS.ViewModels;
using Wpf.Ui.Controls;

namespace StarlightPOS.Views.Pages
{
    /// <summary>
    /// DashboardPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DashboardPage : INavigableView<DashboardViewModel>, INavigationAware
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

        }

        public void OnNavigatedTo()
        {
            ViewModel.LeftGrid = RootLeftGrid;

            ViewModel.Initialize();
        }

        public void OnNavigatedFrom() { }
    }
}
