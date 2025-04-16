using System;
using System.Globalization;
using System.Windows.Data;

namespace TT_Lab.ValueConverters;

public class IsNullConverter : IValueConverter
{
    public Object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null;
    }

    public Object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}