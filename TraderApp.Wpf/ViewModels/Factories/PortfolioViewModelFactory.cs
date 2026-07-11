using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class PortoflioViewModelFactory : ITraderViewModelFactory<PortfolioViewModel>
    {
        public PortfolioViewModel CreateViewModel()
        {
            return new PortfolioViewModel();
        }
    }
}
