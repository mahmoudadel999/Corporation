using System.ComponentModel.DataAnnotations;

namespace Corporation.BLL.Models.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        
        [Display(Name = "Creation date")]
        public DateOnly CreationDate { get; set; }
    }
}
