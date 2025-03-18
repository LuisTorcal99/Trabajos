using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExamenFinal.ViewModel.Main
{
    public class ViewModelBase : ObservableObject
    {
        public virtual Task LoadAsync() => Task.CompletedTask;
    }
}