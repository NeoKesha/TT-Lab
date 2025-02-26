using System;
using System.Globalization;
using System.Windows.Data;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ValueConverters;

public class PrimitiveWrapperConverter<T> : IValueConverter where T : IComparable
{
    public Object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? null : new PrimitiveWrapperViewModel<T>((T)value);
    }

    public Object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? null : ((PrimitiveWrapperViewModel<T>)value).Value;
    }
}

// Because Microsoft didn't implement XAML 2009 standard in compiled markup yet :(

public class PrimitiveWrapperConverterLabUri : PrimitiveWrapperConverter<LabURI>
{
}

public class PrimitiveWrapperConverterInt16 : PrimitiveWrapperConverter<Int16>
{
}

public class PrimitiveWrapperConverterInt32 : PrimitiveWrapperConverter<Int32>
{
}

public class PrimitiveWrapperConverterSingle : PrimitiveWrapperConverter<Single>
{
}

public class PrimitiveWrapperConverterUInt32 : PrimitiveWrapperConverter<UInt32>
{
}