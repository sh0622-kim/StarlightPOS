using StarlightPOS.Managers;
using StarlightPOS.ViewModels;
using Wpf.Ui.Controls;

namespace StarlightPOS.Views.Pages
{
    /// <summary>
    /// OrderPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPage : INavigableView<OrderViewModel>, INavigationAware
    {
        public OrderViewModel ViewModel { get; }

        public OrderPage(OrderViewModel viewModel)
        {
            ViewModel = viewModel;


            InitializeComponent();

        }

        public void OnNavigatedTo()
        {
            ViewModel.Initialize();
            DataContext = this;
        }

        public void OnNavigatedFrom()
        {

        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            DataManager.SaveData("settings\\tables.json", DataManager.Tables);
        }

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            DataManager.SaveData("settings\\tables.json", DataManager.Tables);
        }
    }
}
