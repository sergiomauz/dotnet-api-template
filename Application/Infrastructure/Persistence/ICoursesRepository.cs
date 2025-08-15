using Domain.Entities;
using Domain.QueryObjects;
using Application.Infrastructure.Persistence.Bases;


namespace Application.Infrastructure.Persistence
{
    public interface ICoursesRepository : IBaseWithCodeRepository<Course>
    {
        Task<List<Course>> GetCoursesByTeacherIdAsync(int teacherId, int currentPage, int pageSize);
        Task<int> TotalCoursesByTeacherIdAsync(int teacherId);

        Task<int> TotalCountCoursesByTextFilterAsync(string textFilter);
        Task<List<Course>> SearchCoursesByTextFilterAsync(string textFilter, int currentPage, int pageSize);

        Task<int> TotalCountCoursesByObjectAsync(CoursesQuery coursesQuery);
        Task<IEnumerable<Course>> SearchCoursesByObjectAsync(CoursesPaginatedQuery coursesPaginatedQuery);
    }
}
