using Corporation.DAL.Common.Enums;
using Corporation.DAL.Models.Departments;
using Microsoft.AspNetCore.Http;

namespace Corporation.DAL.Models.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        // FK Column
        public int? DepartmentId { get; set; }

        // Navigational Property [One]
        public virtual Department? Department { get; set; }

        public string? Image { get; set; }
    }
}
