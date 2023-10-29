using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarlightPOS.Managers;
using StarlightPOS.Model;
using StarlightPOS.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Wpf.Ui;

namespace StarlightPOS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory));

            })
            .ConfigureServices((context, services) =>
            {
                DataManager.Setting = DataManager.LoadData<Setting>("settings\\setting.json");
                DataManager.Tables = DataManager.LoadData<ObservableCollection<Table>>("settings\\tables.json");
                DataManager.Products = DataManager.LoadData<ObservableCollection<Product>>("settings\\products.json");
                DataManager.SalesHistory = DataManager.LoadData<ObservableCollection<Table>>("settings\\salesHistory.json");

                // App Host
                services.AddHostedService<ApplicationHostService>();

                // Page resolver service
                services.AddSingleton<IPageService, PageService>();

                // Theme manipulation
                services.AddSingleton<IThemeService, ThemeService>();

                // TaskBar manipulation
                services.AddSingleton<ITaskBarService, TaskBarService>();

                // Service containing navigation
                services.AddSingleton<INavigationService, NavigationService>();

                // Order manipulation
                services.AddSingleton<IOrderService, OrderService>();

                // Main window with navigation
                services.AddSingleton<INavigationWindow, Views.MainWindow>();
                services.AddSingleton<ViewModels.MainWindowViewModel>();

                // Views and ViewModels
                services.AddSingleton<Views.Pages.DashboardPage>();
                services.AddSingleton<ViewModels.DashboardViewModel>();

                // Views and ViewModels
                services.AddSingleton<Views.Pages.SettingsPage>();
                services.AddSingleton<ViewModels.SettingsViewModel>();

                // Views and ViewModels
                services.AddSingleton<Views.Pages.ProductPage>();
                services.AddSingleton<ViewModels.ProductViewModel>();

                // Views and ViewModels
                services.AddSingleton<Views.Pages.OrderPage>();
                services.AddSingleton<ViewModels.OrderViewModel>();

                // Views and ViewModels
                services.AddSingleton<Views.Pages.SalesHistoryPage>();
                services.AddSingleton<ViewModels.SalesHistoryViewModel>();

                // Configuration
                services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
            })
            .Build();

        public static T? GetService<T>() where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        private async void OnStartUp(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}
