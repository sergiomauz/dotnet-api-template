using System.ComponentModel;
using System.Reflection;


namespace Commons.Enums
{
    public static class EnumHelper
    {
        public static TEnum? FromDescription<TEnum>(string description) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr?.Description == description)
                {
                    return (TEnum)field.GetValue(null)!;
                }
            }

            return null;
        }
    }
}
