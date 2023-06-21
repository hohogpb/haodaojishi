using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HaoDaoJiShi.Win32;

namespace HaoDaoJiShi.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RegisterMessages();

            InitObjects();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        // 鼠标拖动窗体
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
       
        // 注册消息
        private void RegisterMessages()
        {
            Messenger.Default.Register<string>(this, "normal", (m) =>
            {
                SizeToContent = SizeToContent.WidthAndHeight;
                WindowState = WindowState.Normal;

                Project.App.Inst.SetMainViewNormal();                
            });

            Messenger.Default.Register<string>(this, "minisize", (m) =>
            {
                SizeToContent = SizeToContent.WidthAndHeight;
                WindowState = WindowState.Normal;

                Project.App.Inst.SetMainViewMinisize();                
            });

            Messenger.Default.Register<string>(this, "fullscreen", (m) =>
            {
                SizeToContent = SizeToContent.Manual;
                WindowState = WindowState.Maximized;

                Project.App.Inst.SetMainViewFullscreen();                
            });

            Messenger.Default.Register<string>(this, "setting", (m) =>
            {
                Views.ViewSetting setting = new Views.ViewSetting();
                setting.DataContext = new ViewModels.ViewSettingModel();
                setting.Owner = this;
                setting.ShowDialog();
            });

            Messenger.Default.Register<string>(this, "exit", (m) =>
            {
                var result = MessageBox.Show(this,"确定要退出么？", "退出", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if( result == MessageBoxResult.Yes)
                    this.Close();
            });

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void InitObjects()
        {
            DataContext = Project.App.Inst.mainWindowModel;

            Project.App.Inst.Init();
        }

    }
}
