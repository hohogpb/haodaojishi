using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HaoDaoJiShi.Project
{
    class SettingNormalView : ViewModelBase
    {
        public SettingNormalView()
        {
            daoJiShiColor = (Color.FromArgb(255, 0, 0, 0));
            daoJiShiBackground = (Color.FromArgb(255, 245, 245, 245));
        }

        public Color daojishi_color;
        public Color daoJiShiColor
        {
            get { return daojishi_color; }
            set
            {
                daojishi_color = value;
                RaisePropertyChanged("daoJiShiColor");
            }
        }

        public Color daojishi_background;
        public Color daoJiShiBackground
        {
            get {
                return daojishi_background;
            }
            set
            {
                daojishi_background = value;
                RaisePropertyChanged("daoJiShiBackground");
            }
        }
    }
}
