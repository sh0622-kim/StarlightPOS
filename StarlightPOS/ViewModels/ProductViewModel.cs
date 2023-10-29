using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using System.Collections.ObjectModel;

namespace StarlightPOS.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Product> _productsCollection = DataManager.Products;


        [RelayCommand]
        private void OnSave(string parameter)
        {
            DataManager.SaveData("settings\\products.json", DataManager.Products);
        }
    }
}
