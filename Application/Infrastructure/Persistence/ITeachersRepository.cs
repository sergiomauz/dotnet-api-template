using Domain.Entities;
using Domain.QueryObjects;
using Application.Infrastructure.Persistence.Bases;


namespace Application.Infrastructure.Persistence
{
    public interface ITeachersRepository : IBaseWithCodeRepository<Teacher>
    {
        Task<int> TotalCountTeachersByTextFilterAsync(string textFilter);
        Task<List<Teacher>> SearchTeachersByTextFilterAsync(string textFilter, int currentPage, int pageSize);

        Task<int> TotalCountTeachersByObjectAsync(TeachersQuery teachersQuery);
        Task<IEnumerable<Teacher>> SearchTeachersByObjectAsync(TeachersPaginatedQuery teachersQuery);
    }
}
