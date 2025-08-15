using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace Persistence.Mapping
{
    public class CourseMap
    {
        public CourseMap(EntityTypeBuilder<Course> entityBuilder)
        {
            entityBuilder.ToTable(name: "Courses");

            #region ======== PRIMARY KEYS ========
            entityBuilder.HasKey(t => t.Id);
            entityBuilder
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region ======== RELATIONSHIPS: HAS MANY ========
            entityBuilder
                .HasMany(d => d.Enrollments)
                .WithOne(o => o.Course)
                .HasForeignKey(to => to.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region ======== NOT MAPPED FIELDS ========
            entityBuilder
                .Ignore(t => t.NotMappedStudents);
            #endregion

            entityBuilder
                .Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);
            entityBuilder
                .HasIndex(t => t.Code)
                .IsUnique();

            entityBuilder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(400);

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
