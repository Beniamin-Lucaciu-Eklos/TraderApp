using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;

namespace TraderApp.Wpf.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
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

                SP500 = await _majorIndexService.GetMajorIndexAsync(MajorIndexType.SP500);
            });
        }

        private MajorIndex _downJones;
        public MajorIndex DownJones
        {
            get => _downJones;
            set
            {
                _downJones = value;
                OnPropertyChanged(nameof(DownJones));
            }
        }

        private MajorIndex _nasdaq;
        public MajorIndex Nasdaq
        {
            get => _nasdaq;
            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }

        private MajorIndex _sp500;
        public MajorIndex SP500
        {
            get => _sp500;
            set
            {
                _sp500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }
    }
}
