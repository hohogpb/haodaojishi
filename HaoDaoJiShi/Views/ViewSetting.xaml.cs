using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HaoDaoJiShi.Views
{
    /// <summary>
    /// ViewSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ViewSetting : Window
    {
        public ViewSetting()
        {
            InitializeComponent();

            RegisterMsgs();
        }

        private void RegisterMsgs()
        {
            Messenger.Default.Register<NotificationMessageAction<string>>(this, "choose-open-file", (m) =>
            {
                OpenFileDialog ofdlg = new OpenFileDialog();
                ofdlg.Filter = m.Notification;
                if (ofdlg.ShowDialog() != true)
                    return ;               
                
                m.Execute(ofdlg.FileName);
            });

            Messenger.Default.Register<NotificationMessageAction<string>>(this, "choose-save-file", (m) =>
            {
                SaveFileDialog ofdlg = new SaveFileDialog();
                ofdlg.Filter = m.Notification;
                if (ofdlg.ShowDialog() != true)
                    return ;               
                
                m.Execute(ofdlg.FileName);
            });

            // 结束时需要卸载这些处理过程
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }
    }
}
