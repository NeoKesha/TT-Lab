using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace TT_Lab.ValueConverters;

public class ListBoxItemConverterIndexed : IValueConverter
{
    public Object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var listBoxItem = value as ListBoxItem;
        var view = ItemsControl.ItemsControlFromItemContainer(listBoxItem) as ListBox;
        var index = view!.ItemContainerGenerator.IndexFromContainer(listBoxItem!);
        
        return listBoxItem!.Content + " " + index;
    }

    public Object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}