using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Assets;

namespace TraderApp.Wpf.ViewModels
{
    public partial class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(assetStore, assetStore => assetStore.Take(3));

            _assetStore.StateChanged += AssetStore_StateChanged;
        }

        public AssetListingViewModel AssetListingViewModel { get; }

        public decimal AccountBalance => _assetStore.AccountBalance;

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }
    }
}
