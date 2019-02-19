using System.ComponentModel.DataAnnotations;

namespace AFTestApp.ViewModels.Enums
{
    public enum DocumentStatus : byte
    {
        [Display(Description = "Открыт")]
        Open = 1,

        [Display(Description = "Закрыт")]
        Submitted = 1
    }
}
