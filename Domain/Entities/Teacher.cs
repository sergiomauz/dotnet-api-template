using Domain.Entities.Bases;


namespace Domain.Entities
{
    public class Teacher : BaseEntityWithIdAndCode
    {
        #region ====== RELATIONSHIPS: ONE TO MANY - HAS MANY ======
        public IEnumerable<Course> Courses { get; set; }
        #endregion

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
