using System.ComponentModel.DataAnnotations;

namespace Corporation.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
