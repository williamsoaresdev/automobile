using System.ComponentModel;
using System.Reflection;

namespace AutomobileRentalManagementAPI.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum valor)
        {
            if (valor == null)
                return String.Empty;

            FieldInfo fieldInfo = valor.GetType().GetField(valor.ToString());

            if (fieldInfo == null)
                return string.Empty;

            DescriptionAttribute[] atributos =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (atributos == null || atributos.Length.Equals(0))
                return valor.ToString();

            var descricao = atributos[0];
            return (descricao.Description.Trim() == String.Empty) ? valor.ToString() : descricao.Description.Trim();
        }

        public static int ToInt32(this Enum valor)
        {
            return Convert.ToInt32(valor);
        }

        public static IEnumerable<string> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value.GetDescription();
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.Equals(description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }

        public static IEnumerable<Enum> GetUniqueFlags(this Enum flags)
        {
            var flag = 1ul;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();

            var name = Enum.GetName(enumType, value);

            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}