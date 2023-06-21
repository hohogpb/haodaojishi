﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HaoDaoJiShi.Converter
{
    class SolidBrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Color))
                throw new InvalidOperationException("Value must be a Color");

            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
#if false
            if (!(value is Color))
                throw new InvalidOperationException("Value must be a Color");

            return new SolidColorBrush((Color)value);
#endif
            throw new NotImplementedException();
        }

    }

}
