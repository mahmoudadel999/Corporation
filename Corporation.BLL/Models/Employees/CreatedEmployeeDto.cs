﻿using Corporation.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Corporation.BLL.Models.Employees
{
    public class CreatedEmployeeDto
    {
        [MaxLength(50, ErrorMessage = "Max length of name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min length of name is 5 chars")]
        public string Name { get; set; } = null!;

        [Range(20, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string? Address { get; set; }

        [Range(4000, 10000)]
        public decimal Salary { get; set; }

        [Display(Name = "Is active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
