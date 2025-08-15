using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace Persistence.Mapping
{
    public class StudentMap
    {
        public StudentMap(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.ToTable(name: "Students");

            #region ======== PRIMARY KEYS ========
            entityBuilder.HasKey(t => t.Id);
            entityBuilder
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region ======== RELATIONSHIPS: HAS MANY ========
            entityBuilder
                .HasMany(d => d.Enrollments)
                .WithOne(o => o.Student)
                .HasForeignKey(to => to.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            entityBuilder
                .Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);
            entityBuilder
                .HasIndex(t => t.Code)
                .IsUnique();

            entityBuilder
                .Property(t => t.Firstname)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(t => t.Lastname)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(t => t.BirthDate)
                .IsRequired();

            #region ======== AUDIT COLUMNS ========
            entityBuilder
                .Property(t => t.CreatedAt)
                .IsRequired();

            entityBuilder
                .Property(t => t.ModifiedAt);
            #endregion
        }
    }
}
