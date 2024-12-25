using System.ComponentModel.DataAnnotations;

namespace Corporation.BLL.Models.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string Gender { get; set; } = null!;

        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;

        public string? Department { get; set; }
    }
}
