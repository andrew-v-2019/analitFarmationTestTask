using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AFTestApp.Services
{
    public static class Extensions
    {
        public static string GetDocumentNumber(this int documentId)
        {
            var docIdString = documentId.ToString();
            const int charsCount = 8;
            const char separator = '0';

            if (docIdString.Length >= charsCount)
            {
                return docIdString;
            }

            docIdString = docIdString.PadLeft(charsCount, separator);

            return docIdString;
        }

        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}
