using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AFTestApp.Extensions
{
    public static class Extensions
    {
        public static void MapFromObjectWithSameNames(this object target, object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sourceFieldInfoes = source.GetType().GetProperties();
            var targetFieldInfoes = target.GetType().GetProperties();
            foreach (var sourceFieldInfo in sourceFieldInfoes)
            {
                var sourceFieldName = sourceFieldInfo.Name;
                var targetFieldinfo = targetFieldInfoes.FirstOrDefault(x => x.Name.Equals(sourceFieldName));
                if (targetFieldinfo == null) continue;
                if (!targetFieldinfo.CanWrite) continue;
                var sourceFieldValue = sourceFieldInfo.GetValue(source);
                if (targetFieldinfo.PropertyType == sourceFieldInfo.PropertyType)
                {
                    targetFieldinfo.SetValue(target, sourceFieldValue);
                }
            }
        }

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
