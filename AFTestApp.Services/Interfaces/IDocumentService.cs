using AFTestApp.DtoModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IDocumentService
    {
        DocumentDto GetNewDocument();
        DocumentDto SubmitDocument(DocumentDto documentViewModel);
    }
}
