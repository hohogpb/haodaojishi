using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HaoDaoJiShi.Win32;

namespace HaoDaoJiShi.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:wxControlsControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:wxControlsControls;assembly=wxControlsControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:BorderLessWindow/>
    ///
    /// resize noborder window
    /// https://www.quppa.net/blog/2010/12/07/windows-7-style-notification-area-applications-in-wpf-part-1-removing-resize/
    /// http://www.codescratcher.com/wpf/set-window-size-dynamically-wpf/
    /// http://denismorozov.blogspot.com/2008/01/how-to-resize-wpf-controls-at-runtime.html
    /// http://nf2p.com/how-to/a-basic-borderless-wpf-window-with-bad-drop-shadow/
    /// https://stackoverflow.com/questions/7369115/maximum-custom-window-loses-drop-shadow-effect
    /// https://stackoverflow.com/questions/13712218/how-to-create-a-wpf-window-without-a-border-that-can-be-resized
    /// https://stackoverflow.com/questions/17645331/wpf-borderless-window-resize ****
    /// </summary>
    public class BorderLessWindow : Window
    {
        const int GripSize = 5;
        const int BorderSize = 3;

        static BorderLessWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderLessWindow), new FrameworkPropertyMetadata(typeof(BorderLessWindow)));
        }

        public BorderLessWindow():base()
        {
          
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource windowSource = HwndSource.FromHwnd(windowHandle);
            windowSource.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_NCHITTEST)
            {
                Point screen_pt = new Point();
                screen_pt.X = lParam.ToInt32() & 0x0000ffff;
                screen_pt.Y = lParam.ToInt32() >> 16;

                Point client_pt = PointFromScreen(screen_pt);

                IntPtr hitted = HitTestResize(client_pt);
                if (hitted != IntPtr.Zero)
                {
                    handled = true;
                    return hitted;
                }
            }
            return IntPtr.Zero;
        }

        private IntPtr HitTestResize(Point mouse_pt)
        {
            // 上边框
            if (mouse_pt.X > GripSize && mouse_pt.X < ActualWidth - GripSize
                && mouse_pt.Y <= BorderSize)
            {
                return (IntPtr)NativeMethods.HTTOP;
            }

            // 下边框
            if (mouse_pt.X > GripSize && mouse_pt.X < ActualWidth - GripSize
                && mouse_pt.Y >= ActualHeight - BorderSize)
            {
                return (IntPtr)NativeMethods.HTBOTTOM;
            }

            // 左边框
            if (mouse_pt.Y > GripSize && mouse_pt.Y < ActualHeight - GripSize
                && mouse_pt.X <= BorderSize)
            {
                return (IntPtr)NativeMethods.HTLEFT;
            }

            // 右边框
            if (mouse_pt.Y > GripSize && mouse_pt.Y < ActualHeight - GripSize
                && mouse_pt.X >= ActualWidth - BorderSize)
            {
                return (IntPtr)NativeMethods.HTRIGHT;
            }

            // 左上角
            if (mouse_pt.X < GripSize && mouse_pt.Y < GripSize)
            {
                return (IntPtr)NativeMethods.HTTOPLEFT;
            }
            // 右上角
            if (mouse_pt.X > ActualWidth - GripSize && mouse_pt.Y < GripSize)
            {
                return (IntPtr)NativeMethods.HTTOPRIGHT;
            }

            // 左下角
            if (mouse_pt.X < GripSize && mouse_pt.Y > ActualHeight - GripSize)
            {
                return (IntPtr)NativeMethods.HTBOTTOMLEFT;
            }
            // 右下角
            if (mouse_pt.X > ActualWidth - GripSize && mouse_pt.Y > ActualHeight - GripSize)
            {
                return (IntPtr)NativeMethods.HTBOTTOMRIGHT;
            }

            if (content_container != null)
            {
                var result = PatchHitTest(this, mouse_pt);

                if (result != null)
                {
                    // A control was hit - it may be the grid if it has a background
                    // texture or gradient over the extended window frame
                    if (result.VisualHit == content_container)
                    {
                        return (IntPtr)NativeMethods.HTCAPTION;
                    }
                }
            }
            return IntPtr.Zero;
        }

        //https://social.msdn.microsoft.com/Forums/en-US/d67d8e80-1d21-4bc8-9c23-e6477e4bc297/is-visualtreehelperhittest-supposed-to-pay-attention-to-ishittestvisible?forum=wpf
        public static HitTestResult PatchHitTest(Visual visual, Point point)
        {
            // This 'HitTest' method also takes the 'IsHitTestVisible' and 'IsVisible' properties
            // into account, so use it instead of the normal VisualTreeHelper.HitTest instead!
            HitTestResult result = null;

            // Use the advanced HitTest method and specify a custom filter that filters out the
            // invisible elements or the elements that don't allow hittesting.
            VisualTreeHelper.HitTest(visual,
                         (target) => {
                             var uiElement = target as UIElement;
                             if ((uiElement != null) && (!uiElement.IsHitTestVisible || !uiElement.IsVisible))
                                 return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
                             else
                                 return HitTestFilterBehavior.Continue;
                         },
                         (target) => {
                             result = target;
                             return HitTestResultBehavior.Stop;
                         },
                         new PointHitTestParameters(point));

            // Return the result
            return result;
        }

        Visual content_container;

        /// <summary>
        /// 重写应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ImageBkToggleButton sticker_btn = GetTemplateChild("stickerBtn") as ImageBkToggleButton;
            ImageBkButton minsize_btn = GetTemplateChild("minsizeBtn") as ImageBkButton;
            ImageBkButton maxsize_btn = GetTemplateChild("maxsizeBtn") as ImageBkButton;
            ImageBkButton restorsize_btn = GetTemplateChild("restorsizeBtn") as ImageBkButton;
            ImageBkButton close_btn = GetTemplateChild("closeBtn") as ImageBkButton;

            if (sticker_btn != null)
                sticker_btn.Click += Sticker_btn_Click;
            if (minsize_btn != null)
                minsize_btn.Click += Minsize_btn_Click;
            if (maxsize_btn != null)
                maxsize_btn.Click += Maxsize_btn_Click; 
            if (restorsize_btn != null)
                restorsize_btn.Click += Restorsize_btn_Click; 
            if (close_btn != null)
                close_btn.Click += Close_btn_Click;

            content_container = GetTemplateChild("clipMask") as Visual;          
        }

        private void Sticker_btn_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)(sender as ToggleButton).IsChecked;            
        }

        private void Minsize_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maxsize_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void Restorsize_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
