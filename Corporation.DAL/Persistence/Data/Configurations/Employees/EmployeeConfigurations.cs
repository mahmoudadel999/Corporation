using Corporation.DAL.Common.Enums;
using Corporation.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporation.DAL.Persistence.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)").HasDefaultValue("Cairo");
            builder.Property(E => E.Salary).HasColumnType("decimal(8, 2)");
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");


            builder.Property(E => E.Gender).HasConversion(
                G => G.ToString(),
                G => (Gender)Enum.Parse(typeof(Gender), G)
                );
            builder.Property(E => E.EmployeeType).HasConversion(
                EmpType => EmpType.ToString(),
                EmpType => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmpType)
                );

        }
    }
}
