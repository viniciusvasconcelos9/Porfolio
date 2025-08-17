using System.ComponentModel;
using System.Reflection;

namespace Clinic_Manager.Core.Enums
{
    public static class EnumsExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType()
                .GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}

