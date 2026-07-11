using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using TraderApp.Application.Services;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Domain.Models;
using TraderApp.FinancialApi.Services;
using TraderApp.Infrastructure.EF;
using TraderApp.Infrastructure.Services;
using TraderApp.Wpf.State.Navigators;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            //using (IServiceScope scope = serviceProvider.CreateScope())
            //{
            //    var different = scope.ServiceProvider.GetRequiredService<MainViewModel>();
            //    var eq = different == mainWindow.DataContext;
            //}

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<TraderDbDesignTimeOptionsFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IStockPriceService, MockStockPriceService>();
            services.AddSingleton<IMajorIndexService, MockMajorIndexService>();

            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSingleton<IRootTraderViewModelFactory, RootTraderViewModelFactory>();
            services.AddSingleton<ITraderViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            services.AddSingleton<ITraderViewModelFactory<PortfolioViewModel>, PortoflioViewModelFactory>();
            services.AddSingleton<ITraderViewModelFactory<MajorIndexListingViewModel>, MajorIndexListingFactoryViewModel>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();
            services.AddScoped<SellViewModel>();

            services.AddScoped<MainWindow>(sp => new MainWindow(sp.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();

        }

    }
}
