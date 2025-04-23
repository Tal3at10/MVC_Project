using System;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.Presistance.Data.Configurations.Employees
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name)
                   .HasColumnType("nvarchar(50)")
                   .IsRequired();

            builder.Property(E => E.Address)
                   .HasColumnType("nvarchar(50)");

            builder.Property(E => E.Salary)
                   .HasColumnType("decimal(8,2)");

            builder.Property(d => d.CreatedOn)
                   .HasDefaultValueSql("GETDATE()");

            // إحنا هنسيب الـ LastModifiedOn يتم تحديثه من الـ App, مش SQL
            // builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            builder.Property(E => E.Gander)
                   .HasConversion(
                       gander => gander.ToString(),
                       gander => (Gander)Enum.Parse(typeof(Gander), gander)
                   );

            builder.Property(E => E.EmployeeType)
                   .HasConversion(
                       empType => empType.ToString(),
                       empType => (EmployeeType)Enum.Parse(typeof(EmployeeType), empType)
                   );
        }
    }
}
