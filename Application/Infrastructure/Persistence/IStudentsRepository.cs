using Domain.Entities;
using Domain.QueryObjects;
using Application.Infrastructure.Persistence.Bases;


namespace Application.Infrastructure.Persistence
{
    public interface IStudentsRepository : IBaseWithCodeRepository<Student>
    {
        Task<int> TotalCountStudentsByTextFilterAsync(string textFilter);
        Task<List<Student>> SearchStudentsByTextFilterAsync(string textFilter, int currentPage, int pageSize);

        Task<int> TotalCountStudentsByObjectAsync(StudentsQuery studentsQuery);
        Task<IEnumerable<Student>> SearchStudentsByObjectAsync(StudentsPaginatedQuery studentsQuery);

        Task<int> TotalStudentsByCourseIdAsync(int courseId);
        Task<List<Enrollment>> GetStudentsByCourseIdAsync(int courseId, int currentPage, int pageSize);
    }
}
