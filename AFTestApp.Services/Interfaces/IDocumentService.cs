
using AFTestApp.ViewModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IDocumentService
    {
        DocumentViewModel GetNewDocument();
        DocumentViewModel SubmitDocument(DocumentViewModel documentViewModel);
    }
}
