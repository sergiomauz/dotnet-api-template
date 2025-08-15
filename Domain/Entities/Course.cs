using Domain.Entities.Bases;


namespace Domain.Entities
{
    public class Course : BaseEntityWithIdAndCode
    {
        #region ====== RELATIONSHIPS: ONE TO MANY - HAS MANY ======
        public IEnumerable<Enrollment> Enrollments { get; set; }
        #endregion

        #region ====== RELATIONSHIPS: BELONGS TO ======
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        #endregion

        #region ====== RELATIONSHIPS AND NOT MAPPED ======
        public int? NotMappedStudents { get; set; }
        #endregion

        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
