using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDaoJiShi.ViewModels
{
    /// <summary>
    /// 主窗体vm基类，因为他们三个有大段重复代码
    /// </summary>
    class ViewMainModelBase : ViewModelBase
    {
        public Project.Session session
        {
            get { return Project.App.Inst.session; }
        }

        public ViewMainModelBase()
        {
            cmdStartCount = new RelayCommand<object>(this.OnCmdStartCount);
            cmdResetCount = new RelayCommand<object>(this.OnCmdResetCount);
            cmdPauseContinueCount = new RelayCommand<object>(this.OnCmdPauseContinueCount);

            cmdNormal = new RelayCommand<object>(this.OnCmdNormal);
            cmdWinMinisize = new RelayCommand<object>(this.OnCmdWinMinisize);
            cmdWinFullscreen = new RelayCommand<object>(this.OnCmdWinFullscreen);
            cmdSysSetting = new RelayCommand<object>(this.OnCmdSysSetting);
            cmdSysExit = new RelayCommand<object>(this.OnCmdSysExit);
        }

        public RelayCommand<object> cmdStartCount { get; set; }
        public RelayCommand<object> cmdResetCount { get; set; }
        public RelayCommand<object> cmdPauseContinueCount { get; set; }
        public RelayCommand<object> cmdNormal { get; set; }
        public RelayCommand<object> cmdWinMinisize { get; set; }
        public RelayCommand<object> cmdWinFullscreen { get; set; }
        public RelayCommand<object> cmdSysSetting { get; set; }
        public RelayCommand<object> cmdSysExit { get; set; }

        protected void OnCmdStartCount(object obj)
        {
            session.daoJiShi.StartCount();            
        }

        protected void OnCmdResetCount(object obj)
        {
           session.daoJiShi.ResetCount();
        }

        protected void OnCmdPauseContinueCount(object obj)
        {
           session.daoJiShi.PauseContinueCount();
        }

        protected void OnCmdWinMinisize(object obj)
        {
            Messenger.Default.Send<string>("minisize", "minisize");
        }

        protected void OnCmdWinFullscreen(object obj)
        {
            Messenger.Default.Send<string>("fullscreen", "fullscreen");
        }

        protected void OnCmdSysSetting(object obj)
        {
            Messenger.Default.Send<string>("setting", "setting");
        }
        protected void OnCmdSysExit(object obj)
        {
            Messenger.Default.Send<string>("exit", "exit");
        }

        protected void OnCmdNormal(object obj)
        {
            Messenger.Default.Send<string>("normal", "normal");
        }
    }
}
