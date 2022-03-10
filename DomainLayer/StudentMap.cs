using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class StudentMap:IEntityTypeConfiguration<Student>
    {
    public void Configure(EntityTypeBuilder<Student> builder)
            {
            builder.HasKey(x => x.Id).HasName("pk_studentid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("Id").HasColumnType("INT");
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.LastName).HasColumnName("LastName").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.Gender).HasColumnName("Gender").HasColumnType("VARCHAR(30)");
            builder.Property(x => x.Age).HasColumnName("Age").HasColumnType("INT");
            builder.Property(x => x.Address).HasColumnName("Address").HasColumnType("NVARCHAR(100)");

        }
    }
}
