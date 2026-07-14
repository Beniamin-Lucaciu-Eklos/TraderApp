using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Wpf.ViewModels
{
    public partial class MessageViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasMessage))]
        private string _message = string.Empty;

        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    }
}
