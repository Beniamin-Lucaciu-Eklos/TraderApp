using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public interface IRootTraderViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
