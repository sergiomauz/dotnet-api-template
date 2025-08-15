using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace Persistence.Mapping
{
    public class TeacherMap
    {
        public TeacherMap(EntityTypeBuilder<Teacher> entityBuilder)
        {
            entityBuilder.ToTable(name: "Teachers");

            #region ======== PRIMARY KEYS ========
            entityBuilder.HasKey(t => t.Id);
            entityBuilder
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region ======== RELATIONSHIPS: HAS MANY ========
            entityBuilder
                .HasMany(d => d.Courses)
                .WithOne(o => o.Teacher)
                .HasForeignKey(to => to.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);
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
