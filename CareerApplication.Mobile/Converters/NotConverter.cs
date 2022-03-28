namespace CareerApplication.Mobile.Converters;

public class NotConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isValue = (bool)value;
        return !isValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isValue = (bool)value;
        return !isValue;
    }
}
