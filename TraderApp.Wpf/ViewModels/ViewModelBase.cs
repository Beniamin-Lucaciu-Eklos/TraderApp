using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Wpf.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>();    
   
    public partial class ViewModelBase : ObservableObject
    {
    }
}
