using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDaoJiShi.ViewModels
{
    class MainWindowModel : ViewModelBase
    {        
        private ViewModelBase current_view_model;

        public ViewModelBase currentViewModel
        {
            get { return current_view_model; }

            set
            {
                current_view_model = value;
                RaisePropertyChanged("currentViewModel");
            }
        }
    }
}
