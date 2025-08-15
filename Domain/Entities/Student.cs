using Domain.Entities.Bases;


namespace Domain.Entities
{
    public class Student : BaseEntityWithIdAndCode
    {
        #region ====== RELATIONSHIPS: ONE TO MANY - HAS MANY ======
        public IEnumerable<Enrollment> Enrollments { get; set; }
        #endregion

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
