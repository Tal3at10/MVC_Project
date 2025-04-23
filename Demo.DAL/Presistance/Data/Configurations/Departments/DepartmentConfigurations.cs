using System;
using Demo.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.Presistance.Data.Configurations.Departments
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(d => d.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Code).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");



        }
    }
}
