using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;

namespace TraderApp.Wpf.ViewModels
{
    public partial class MajorIndexListingViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexModel(IMajorIndexService majorIndexService)
        {
            var viewModel = new MajorIndexListingViewModel(majorIndexService);
            viewModel.LoadMajorIndices();
            return viewModel;
        }

        private void LoadMajorIndices()
        {
            Task.Run(async () =>
            {
                DownJones = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.Dowjones);

                Nasdaq = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.Nasdaq);

                Sp500 = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.SP500);
            });
        }

        [ObservableProperty]
        private MajorIndex _downJones;

        [ObservableProperty]
        private MajorIndex _nasdaq;

        [ObservableProperty]
        private MajorIndex _sp500;
    }
}
