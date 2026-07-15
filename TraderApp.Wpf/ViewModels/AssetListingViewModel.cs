using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Assets;

namespace TraderApp.Wpf.ViewModels
{
    public partial class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly ObservableCollection<AssetViewModel> _assets;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;

        public AssetListingViewModel(AssetStore assetStore)
            : this(assetStore, assets => assets) { }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _filterAssets = filterAssets;

            _assets = new ObservableCollection<AssetViewModel>();
            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        public IEnumerable<AssetViewModel> Assets => _assets;

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(x => x.Asset.Symbol)
                .Select(g =>
                    new AssetViewModel(g.Key,
                        g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assetViewModels = _filterAssets(assetViewModels);

            _assets.Clear();

            foreach (AssetViewModel assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }
    }
}
