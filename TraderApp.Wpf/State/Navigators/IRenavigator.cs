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
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public ViewModelFactoryRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }


        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}
