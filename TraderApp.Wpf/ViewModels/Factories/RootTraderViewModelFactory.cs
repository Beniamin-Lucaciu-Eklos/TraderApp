using System.Reflection.Metadata;
using TraderApp.FinancialApi.Services;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class RootTraderViewModelFactory : IRootTraderViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortofolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly BuyViewModel _buyViewModel;
        private readonly SellViewModel _sellViewModel;

        public RootTraderViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<PortfolioViewModel> createPortofolioViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel,
            BuyViewModel buyViewModel,
            SellViewModel sellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortofolioViewModel = createPortofolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _buyViewModel = buyViewModel;
            _sellViewModel = sellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home
                    => _createHomeViewModel(),

                ViewType.Portofolio
                    => _createPortofolioViewModel(),


                ViewType.Login
                    => _createLoginViewModel(),

                ViewType.Buy
                    => _buyViewModel,

                ViewType.Sell
                    => _sellViewModel,

                _ => throw new ArgumentException("View type does not have a viewmodel", "viewType")
            };
        }
    }
}
