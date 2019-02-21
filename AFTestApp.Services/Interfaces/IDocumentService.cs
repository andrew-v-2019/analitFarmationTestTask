using AFTestApp.DtoModels;
using AFTestApp.ViewModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IDocumentService
    {
        DocumentDto GetNewDocument();
        SubmitResultDto SubmitDocument(DocumentDto documentViewModel);
    }
}
