using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using TraderApp.Wpf.State.Accounts;
using TraderApp.Wpf.State.Assets;
using TraderApp.Wpf.State.Authentication;
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
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<TraderDbContext>(n => n.UseSqlServer(connectionString));
                    services.AddSingleton<TraderDbDesignTimeOptionsFactory>(new TraderDbDesignTimeOptionsFactory(connectionString));

                    services.AddSingleton<IAuthenticationService, AuthenticationService>();
                    services.AddSingleton<IAccountService, AccountDataService>();
                    services.AddSingleton<IBuyStockService, BuyStockService>();
                    services.AddSingleton<IStockPriceService, MockStockPriceService>();
                    services.AddSingleton<IMajorIndexService, MockMajorIndexService>();

                    services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

                    services.AddSingleton<IRootTraderViewModelFactory, RootTraderViewModelFactory>();
                    services.AddSingleton<BuyViewModel>();
                    services.AddSingleton<PortfolioViewModel>();
                    services.AddSingleton<AssetSummaryViewModel>();

                    services.AddSingleton<HomeViewModel>(services =>
                    {
                        AssetSummaryViewModel assetSummaryViewModel = services.GetRequiredService<AssetSummaryViewModel>();
                        return new HomeViewModel(MajorIndexListingViewModel.LoadMajorIndexModel(services.GetRequiredService<IMajorIndexService>()), assetSummaryViewModel);
                    });
                    services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
                    {
                        return () => services.GetRequiredService<HomeViewModel>();
                    });

                    services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
                    {
                        return () => services.GetRequiredService<BuyViewModel>();
                    });
                    services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
                    {
                        return () => services.GetRequiredService<PortfolioViewModel>();
                    });
                    services.AddSingleton<CreateViewModel<SellViewModel>>(services =>
                    {
                        return () => services.GetRequiredService<SellViewModel>();
                    });

                    services.AddSingleton<ViewModelFactoryRenavigator<LoginViewModel>>();
                    services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => {
                        return () => new RegisterViewModel(
                            services.GetRequiredService<IAuthenticator>(),
                            services.GetRequiredService<ViewModelFactoryRenavigator<LoginViewModel>>());
                    });

                    services.AddSingleton<ViewModelFactoryRenavigator<HomeViewModel>>();
                    services.AddSingleton<ViewModelFactoryRenavigator<RegisterViewModel>>();
                    services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
                    {
                        return () => new LoginViewModel(
                            services.GetRequiredService<IAuthenticator>(),
                            services.GetRequiredService<ViewModelFactoryRenavigator<HomeViewModel>>(),
                            services.GetRequiredService<ViewModelFactoryRenavigator<RegisterViewModel>>()
                            );
                    });

                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IAccountStore, AccountStore>();
                    services.AddSingleton<IAuthenticator, Authenticator>();
                    services.AddSingleton<AssetStore>();
                    services.AddScoped<MainViewModel>();
                    services.AddSingleton<SellViewModel>();

                    services.AddScoped<MainWindow>(sp => new MainWindow(sp.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            //Task.Run(async () =>
            //{
            //    var auth = serviceProvider.GetRequiredService<IAuthenticationService>();
            //   var registerationResult = await auth.Register("ben@ben.ro", "benLuc", "test123", "test123");

            //});
            MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            //using (IServiceScope scope = serviceProvider.CreateScope())
            //{
            //    var different = scope.ServiceProvider.GetRequiredService<MainViewModel>();
            //    var eq = different == mainWindow.DataContext;
            //}

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

    }
}
