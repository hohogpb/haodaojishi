using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDaoJiShi.Project
{
    class App
    {
        static private App inst = new App();

        static public App Inst
        {
            get { return inst; }
        }

        /////////////////////////////////////////////////////
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /////////////////////////////////////////////////////////////
        public ViewModels.MainWindowModel mainWindowModel { get; set; }
        public ViewModels.ViewMaxsizeModel viewMaxsizeModel { get; set; }
        public ViewModels.ViewMinSizeModel viewMinSizeModel { get; set; }
        public ViewModels.ViewNormalModel viewNormalModel { get; set; }

        /////////////////////////////////////////////////////////////
        public Session session { get; set; }

        public App()
        {
            mainWindowModel = new ViewModels.MainWindowModel();
            viewMaxsizeModel = new ViewModels.ViewMaxsizeModel();
            viewMinSizeModel = new ViewModels.ViewMinSizeModel();
            viewNormalModel = new ViewModels.ViewNormalModel();                      
        }
        
        internal void Init()
        {
            SetMainViewNormal();

            SessionLoaderSaver.Load();
        }

        internal void End()
        {
            SessionLoaderSaver.Save();
        }

        internal void SetMainViewNormal()
        {
            mainWindowModel.currentViewModel = viewNormalModel;
        }

        internal void SetMainViewMinisize()
        {
            mainWindowModel.currentViewModel = viewMinSizeModel;
        }

        internal void SetMainViewFullscreen()
        {
            mainWindowModel.currentViewModel = viewMaxsizeModel;
        }


    }
}
