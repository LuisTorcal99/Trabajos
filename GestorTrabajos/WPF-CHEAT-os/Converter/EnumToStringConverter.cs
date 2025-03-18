using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using WPF_CHEAT_os.Utils;

public class EnumToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            return GetEnumDescription(enumValue);
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && !string.IsNullOrWhiteSpace(str))
        {
            foreach (var field in targetType.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute?.Description == str || field.Name == str)
                {
                    return Enum.Parse(targetType, field.Name);
                }
            }
        }
        return EstadoPropuesta.Enviada; // Valor por defecto
    }

    private static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
