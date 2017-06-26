using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using IWManager.BL.Models;

namespace IWManager.App.Converters
{
    public class GeneralRatingListsToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<GeneralRatingListModel> generalRatings = value as IEnumerable<GeneralRatingListModel>;
            return generalRatings?.Select(r => new ComboBoxItem { Tag = r.Id, Content = $"{r.Title}" });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}