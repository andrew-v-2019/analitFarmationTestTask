using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AFTestApp.ViewModels.Enums
{
    public enum DocumentStatus : byte
    {
        [Display(Description = "Открыт")]
        [Description("Открыт")]
        Open = 1,

        [Display(Description = "Закрыт")]
        [Description("Закрыт")]
        Submitted = 1
    }
}
