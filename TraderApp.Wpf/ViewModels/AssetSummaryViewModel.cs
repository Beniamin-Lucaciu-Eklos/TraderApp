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
        private readonly ObservableCollection<AssetViewModel> _topAssets;

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;

            _topAssets = new ObservableCollection<AssetViewModel>();
            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        public decimal AccountBalance => _assetStore.AccountBalance;

        public IEnumerable<AssetViewModel> TopAssets => _topAssets;

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
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
                .OrderByDescending(a => a.Shares)
                .Take(3);

            _topAssets.Clear();

            foreach (AssetViewModel assetViewModel in assetViewModels)
            {
                _topAssets.Add(assetViewModel);
            }
        }
    }
}
