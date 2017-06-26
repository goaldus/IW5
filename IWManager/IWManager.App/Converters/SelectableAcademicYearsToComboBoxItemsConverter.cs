using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace IWManager.App.Converters
{
    public class SelectableAcademicYearsToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<int> years = value as IEnumerable<int>;
            return years?.Select(y => new ComboBoxItem { Tag = y, Content = $"{y}/{y+1}" });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}