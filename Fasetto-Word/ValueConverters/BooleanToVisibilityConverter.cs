namespace Fasetto_Word.ValueConverters;

using System;
using System.Globalization;
using System.Windows;

public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is "true_visible")
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        else
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}