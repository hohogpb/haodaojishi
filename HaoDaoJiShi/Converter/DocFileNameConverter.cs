using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;

namespace HaoDaoJiShi.Converter
{
    class DocFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string before = (string)value;

            string after = string.Empty;

            if (before == null || before.Length == 0)
            {
                after = "!!!系统内置方案,建议保存方案";
            }
            else
            {
                after = Path.GetFileName(before);
            }
                        
            return '[' + after + ']';
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
