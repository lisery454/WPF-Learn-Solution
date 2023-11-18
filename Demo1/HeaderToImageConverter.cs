using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Demo1.Directory.Data;

namespace Demo1;

[ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
public class HeaderToImageConverter : IValueConverter
{
    public static readonly HeaderToImageConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var itemType = (DirectoryItemType)value;

        var image = itemType switch
        {
            DirectoryItemType.Drive => "drive.png",
            DirectoryItemType.File => "file.png",
            DirectoryItemType.Folder => "folder-closed.png",
            _ => throw new ArgumentOutOfRangeException()
        };

        return new BitmapImage(new Uri($"pack://application:,,,/Images/{image}"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new object();
    }
}