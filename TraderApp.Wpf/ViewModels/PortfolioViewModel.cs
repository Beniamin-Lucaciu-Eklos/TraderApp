using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Assets;

namespace TraderApp.Wpf.ViewModels
{
    public partial class PortfolioViewModel : ViewModelBase
    {
        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }

        public AssetListingViewModel AssetListingViewModel { get; }
    }
}
