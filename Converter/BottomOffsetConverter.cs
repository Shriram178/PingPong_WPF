using System.Globalization;
using System.Windows.Data;

namespace BounceBall.Converter
{
    public class BottomOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double canvasHeight)
            {
                return canvasHeight - 10;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



