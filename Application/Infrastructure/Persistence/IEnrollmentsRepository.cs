using Domain.Entities;
using Application.Infrastructure.Persistence.Bases;


namespace Application.Infrastructure.Persistence
{
    public interface IEnrollmentsRepository : IBaseWithGuidRepository<Enrollment>
    {
        Task<Enrollment?> GetEnrollmentsByStudentIdAsync(int courseId, int studentId);
        Task<List<Enrollment>> GetCoursesByStudentIdAsync(int studentId, int currentPage, int pageSize);
        Task<int> TotalCountCoursesByStudentIdAsync(int studentId);
        Task<List<Enrollment>> GetStudentsByTeacherIdAsync(int teacherId, int currentPage, int pageSize);
        Task<int> TotalCountStudentsByTeacherIdAsync(int teacherId);
    }
}
