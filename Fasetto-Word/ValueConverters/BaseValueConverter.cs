﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Fasetto_Word;

public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new()
{
    private static T? _converter;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _converter ??= new T();
    }


    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}