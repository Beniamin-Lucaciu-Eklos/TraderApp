using System.Reflection.Metadata;
using TraderApp.FinancialApi.Services;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class RootTraderViewModelFactory : IRootTraderViewModelFactory
    {
        private readonly ITraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ITraderViewModelFactory<PortfolioViewModel> _portofolioViewModelFactory;
        private readonly BuyViewModel _buyViewModel;
        private readonly SellViewModel _sellViewModel;

        public RootTraderViewModelFactory(
            ITraderViewModelFactory<HomeViewModel> homeViewModelFactory,
            ITraderViewModelFactory<PortfolioViewModel> portofolioViewModelFactory,
            BuyViewModel buyViewModel,
            SellViewModel sellViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portofolioViewModelFactory = portofolioViewModelFactory;
            _buyViewModel = buyViewModel;
            _sellViewModel = sellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home
                    => _homeViewModelFactory.CreateViewModel(),

                ViewType.Portofolio
                    => _portofolioViewModelFactory.CreateViewModel(),

                ViewType.Buy
                    => _buyViewModel,

                ViewType.Sell
                    => _sellViewModel,

                _ => throw new ArgumentException("View type does not have a viewmodel", "viewType")
            };
        }
    }
}
