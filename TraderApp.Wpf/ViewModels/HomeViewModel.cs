using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;

namespace TraderApp.Wpf.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(MajorIndexListingViewModel majorIndexViewModel)
        {
            MajorIndexViewModel = majorIndexViewModel;
        }

        public MajorIndexListingViewModel MajorIndexViewModel { get; private set; }
    }
}
