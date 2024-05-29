using System.ComponentModel.DataAnnotations;

namespace Ekas.Monitoring.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            if (value == null) return string.Empty; 
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            if (attributes != null && attributes.Length > 0) return attributes[0].Name;
            return value.ToString();
        }
    }
}
