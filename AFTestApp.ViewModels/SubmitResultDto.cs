using AFTestApp.DtoModels;

namespace AFTestApp.ViewModels
{
    public class SubmitResultDto
    {
        public int TotalDocumentsCount { get; set; }
        public DocumentDto Document { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
