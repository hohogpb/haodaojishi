using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HaoDaoJiShi.Project
{
    class Session : ViewModelBase
    {

        public Session()
        {        
            daoJiShi = new DaoJiShi();

            settingFullscreen = new SettingFullscreen();
            settingNormalView = new SettingNormalView();
            settingMiniView = new SettingMiniView();
        }

        string doc_file_path;

        public string docFilePath
        {
            get { return doc_file_path; }
            set
            {
                doc_file_path = value;
                RaisePropertyChanged("docFilePath");
            }
        }

        DaoJiShiDoc _doc;

        public DaoJiShiDoc doc
        {
            get { return _doc; }
            set
            {
                _doc = value;

                RaisePropertyChanged("doc");

                daoJiShi.doc = _doc;
            }
        }

        public DaoJiShi daoJiShi { get; set; }

        // 全屏
        private SettingFullscreen setting_fullscreen;
        public SettingFullscreen settingFullscreen
        {
            get { return setting_fullscreen; }
            set
            {
                setting_fullscreen = value;
                RaisePropertyChanged("settingFullscreen");
            }
        }

        // 正常
        private SettingNormalView setting_normal_view;
        public SettingNormalView settingNormalView
        {
            get { return setting_normal_view; }
            set
            {
                setting_normal_view = value;
                RaisePropertyChanged("settingNormalView");
            }
        }

        // mini
        private SettingMiniView setting_miniview;
        public SettingMiniView settingMiniView
        {
            get { return setting_miniview; }
            set
            {
                setting_miniview = value;
                RaisePropertyChanged("settingMiniView");
            }
        }


    }
}
