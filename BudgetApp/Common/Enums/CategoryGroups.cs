using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum CategoryGroups
    {
        [Display(Name = "Расходы")]
        Expenses = 0,

        [Display(Name = "Доходы")]
        Income = 1
    }
}
