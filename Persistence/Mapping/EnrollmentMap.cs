using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;


namespace Persistence.Mapping
{
    public class EnrollmentMap
    {
        public EnrollmentMap(EntityTypeBuilder<Enrollment> entityBuilder)
        {
            entityBuilder.ToTable(name: "Enrollments");

            #region ======== PRIMARY KEYS ========
            entityBuilder.HasKey(t => t.Id);
            entityBuilder
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            #endregion

            #region ======== AUDIT COLUMNS ========
            entityBuilder
                .Property(t => t.CreatedAt)
                .IsRequired();
            #endregion
        }
    }
}
