using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.State.Navigators
{
    public interface IRenavigator
    {
        void Renavigate();
    }

    public class ViewModelFactoryRenavigator<TViewModel>
        : IRenavigator
         where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly ITraderViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator,
            ITraderViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
