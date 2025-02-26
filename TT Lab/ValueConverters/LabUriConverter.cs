using System;
using System.Globalization;
using System.Windows.Data;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ValueConverters;

public class LabUriConverter : IValueConverter
{
    public Object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var assetManager = AssetManager.Get();
        switch (value)
        {
            case LabURI labUri:
                return labUri == LabURI.Empty ? "Empty" : assetManager.GetAsset(labUri).Alias;
            case PrimitiveWrapperViewModel<LabURI> wrapper:
                return wrapper.Value == LabURI.Empty ? "Empty" : assetManager.GetAsset(wrapper.Value).Alias;
            case null:
                return "Empty";
        }

        return $"Provided type was {value.GetType().Name}. Check your bindings!";
    }

    public Object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}