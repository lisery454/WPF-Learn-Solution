using System;
using System.Globalization;
using Fasetto_Word.DataModels;
using Fasetto_Word.Pages;

namespace Fasetto_Word.ValueConverters;

public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (ApplicationPage)value switch
        {
            ApplicationPage.Login => new LoginPage(),
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}