using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDaoJiShi
{
    class Window1ViewModel : ViewModelBase
    {
        private string test_text;

        public string testText
        {
            get { return test_text; }

            set {
                test_text = value;
                RaisePropertyChanged("testText");
            }
        }


    }
}
