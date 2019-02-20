using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AFTestApp.ViewModels.Enums
{
    public enum DocumentType : byte
    {
        [Display(Description = "Продажа")]
        [Description("Продажа")]
        Sale = 1
    }
}
