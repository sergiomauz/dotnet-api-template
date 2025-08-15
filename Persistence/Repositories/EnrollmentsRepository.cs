using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Persistence.Repositories.Bases;
using Application.Infrastructure.Persistence;


namespace Persistence.Repositories
{
    public class EnrollmentsRepository : BaseWithGuidRepository<Enrollment>, IEnrollmentsRepository
    {
        private readonly SqlServerDbContext _sqlServerDbContext;

        public EnrollmentsRepository(SqlServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<Enrollment?> GetEnrollmentsByStudentIdAsync(int courseId, int studentId)
        {
            var entity = await _sqlServerDbContext.Set<Enrollment>().SingleOrDefaultAsync(t => t.CourseId == courseId && t.StudentId == studentId);

            return entity;
        }

        public async Task<List<Enrollment>> GetCoursesByStudentIdAsync(int studentId, int currentPage, int pageSize)
        {
            var enrollments = await (from co in _sqlServerDbContext.Set<Course>()
                                     join en in _sqlServerDbContext.Set<Enrollment>() on co.Id equals en.CourseId
                                     join te in _sqlServerDbContext.Set<Teacher>() on co.TeacherId equals te.Id
                                     where en.StudentId == studentId
                                     orderby en.CreatedAt descending
                                     select new Enrollment
                                     {
                                         Course = new Course
                                         {
                                             Code = co.Code,
                                             Name = co.Name,
                                             Teacher = new Teacher
                                             {
                                                 Firstname = te.Firstname,
                                                 Lastname = te.Lastname
                                             }
                                         },
                                         CreatedAt = en.CreatedAt
                                     })
                                   .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                   .Take(Convert.ToInt32(pageSize))
                                   .ToListAsync();

            return enrollments;
        }

        public async Task<int> TotalCountCoursesByStudentIdAsync(int studentId)
        {
            var count = await (from en in _sqlServerDbContext.Set<Enrollment>()
                               where en.StudentId == studentId
                               select en)
                               .CountAsync();

            return count;
        }

        public async Task<List<Enrollment>> GetStudentsByTeacherIdAsync(int teacherId, int currentPage, int pageSize)
        {
            var enrollments = await (from co in _sqlServerDbContext.Set<Course>()
                                     join en in _sqlServerDbContext.Set<Enrollment>() on co.Id equals en.CourseId
                                     join st in _sqlServerDbContext.Set<Student>() on en.StudentId equals st.Id
                                     where co.TeacherId == teacherId
                                     orderby en.CreatedAt descending
                                     select new Enrollment
                                     {
                                         Course = new Course
                                         {
                                             Name = co.Name
                                         },
                                         Student = new Student
                                         {
                                             Code = st.Code,
                                             Firstname = st.Firstname,
                                             Lastname = st.Lastname
                                         }
                                     })
                                   .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                   .Take(Convert.ToInt32(pageSize))
                                   .ToListAsync();

            return enrollments;
        }

        public async Task<int> TotalCountStudentsByTeacherIdAsync(int teacherId)
        {
            var count = await (from en in _sqlServerDbContext.Set<Enrollment>()
                               join co in _sqlServerDbContext.Set<Course>() on en.CourseId equals co.Id
                               where co.TeacherId == teacherId
                               select en)
                               .CountAsync();

            return count;
        }
    }
}
