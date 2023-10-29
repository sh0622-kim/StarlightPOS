using StarlightPOS.ViewModels;
using Wpf.Ui.Controls;

namespace StarlightPOS.Views.Pages
{
    /// <summary>
    /// ProductPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProductPage : INavigableView<ProductViewModel>
    {
        public ProductViewModel ViewModel { get; }

        public ProductPage(ProductViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
