using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class HomeViewModelFactory(ITraderViewModelFactory<MajorIndexListingViewModel> majorIndexFactory) : ITraderViewModelFactory<HomeViewModel>
    {
        public HomeViewModel CreateViewModel()
            => new HomeViewModel(majorIndexFactory.CreateViewModel());
    }
}
