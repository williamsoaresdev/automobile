using AutomobileRentalManagementAPI.CrossCutting.Extensions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AutomobileRentalManagementAPI.CrossCutting.Util
{
    public static class ValidationUtil
    {
        public static bool IsValidCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14 || new string(cnpj[0], 14) == cnpj)
                return false;

            int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string baseCnpj = cnpj[..12];
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(baseCnpj[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string checkDigits = remainder.ToString();
            baseCnpj += checkDigits;

            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(baseCnpj[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            checkDigits += remainder.ToString();

            return cnpj.EndsWith(checkDigits);
        }

        public static bool IsValidEnumDescription<TEnum>(string value) where TEnum : Enum
        {
            var descriptions = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e =>
                {
                    var descriptionAttribute = typeof(TEnum)
                        .GetField(e.ToString())
                        ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    return descriptionAttribute?.Description ?? e.ToString();
                });

            return descriptions.Contains(value);
        }

        public static bool IsValidEnumDescription<TEnum>(TEnum value) where TEnum : Enum
        {
            var validDescriptions = new HashSet<string>(
                Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Select(e => e.GetDescription()));

            return validDescriptions.Contains(value.GetDescription());
        }



    }
}