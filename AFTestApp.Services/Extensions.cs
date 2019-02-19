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
    }
}
