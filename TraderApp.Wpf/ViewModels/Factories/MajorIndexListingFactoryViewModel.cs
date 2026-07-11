using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class MajorIndexListingFactoryViewModel(IMajorIndexService majorIndexService) : ITraderViewModelFactory<MajorIndexListingViewModel>
    {
        public MajorIndexListingViewModel CreateViewModel()
        {
            return MajorIndexListingViewModel.LoadMajorIndexModel(majorIndexService);
        }
    }
}
