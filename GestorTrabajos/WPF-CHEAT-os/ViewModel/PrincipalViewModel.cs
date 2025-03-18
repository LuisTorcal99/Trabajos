using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class PrincipalViewModel : ViewModelBase
    {
        public PrincipalViewModel()
        {

        }
        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
