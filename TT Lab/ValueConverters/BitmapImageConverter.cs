using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TT_Lab.ValueConverters;

public class BitmapImageConverter : IValueConverter
{
    public Object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Bitmap bp)
        {
            return null;
        }

        using var memory = new MemoryStream();
        bp.Save(memory, ImageFormat.Png);
        memory.Position = 0;
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memory;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        
        return bitmapImage;
    }

    public Object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}