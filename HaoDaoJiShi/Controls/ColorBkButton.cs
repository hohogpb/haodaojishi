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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HaoDaoJiShi.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:wpf_weixin.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:wpf_weixin.Controls;assembly=wpf_weixin.Controls"
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
    ///     <MyNamespace:wxColorButton/>
    ///
    /// </summary>
    public class ColorBkButton : Button
    {
        static ColorBkButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorBkButton), new FrameworkPropertyMetadata(typeof(ColorBkButton)));
        }

        /// <summary>
        /// 普通背景色
        /// </summary>
        public static readonly DependencyProperty BrushBkNormalProperty =
            DependencyProperty.Register("BrushBkNormal", typeof(Color), typeof(ColorBkButton), new PropertyMetadata(default(Color)));

        public Color BrushBkNormal
        {
            get { return (Color)GetValue(BrushBkNormalProperty); }
            set { SetValue(BrushBkNormalProperty, value); }
        }

        /// <summary>
        /// 鼠标移过背景色
        /// </summary>
        public static readonly DependencyProperty BrushBkMouseOnProperty =
            DependencyProperty.Register("BrushBkMouseOn", typeof(Color), typeof(ColorBkButton), new PropertyMetadata(default(Color)));

        public Color BrushBkMouseOn
        {
            get { return (Color)GetValue(BrushBkMouseOnProperty); }
            set { SetValue(BrushBkMouseOnProperty, value); }
        }

        /// <summary>
        /// 鼠标按下背景色
        /// </summary>
        public static readonly DependencyProperty BrushBkMousePressProperty =
            DependencyProperty.Register("BrushBkMousePress", typeof(Color), typeof(ColorBkButton), new PropertyMetadata(default(Color)));

        public Color BrushBkMousePress
        {
            get { return (Color)GetValue(BrushBkMousePressProperty); }
            set { SetValue(BrushBkMousePressProperty, value); }
        }

    }
}
