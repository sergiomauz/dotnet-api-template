using System.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Commons.Enums;
using Domain.Entities;
using Domain.QueryObjects;
using Persistence.Repositories.Bases;
using Application.Infrastructure.Persistence;


namespace Persistence.Repositories
{
    public class StudentsRepository : BaseWithCodeRepository<Student>, IStudentsRepository
    {
        private readonly SqlServerDbContext _sqlServerDbContext;

        public StudentsRepository(SqlServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<int> TotalStudentsByCourseIdAsync(int courseId)
        {
            var count = await (from en in _sqlServerDbContext.Set<Enrollment>()
                               where en.CourseId == courseId
                               select en)
                              .CountAsync();

            return count;
        }

        public async Task<List<Enrollment>> GetStudentsByCourseIdAsync(int courseId, int currentPage, int pageSize)
        {
            var courses = await (from en in _sqlServerDbContext.Set<Enrollment>()
                                 join st in _sqlServerDbContext.Set<Student>() on en.StudentId equals st.Id
                                 where en.CourseId == courseId
                                 orderby en.CreatedAt descending
                                 select new Enrollment
                                 {
                                     Id = en.Id,
                                     CreatedAt = en.CreatedAt,
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

            return courses;
        }

        public async Task<int> TotalCountStudentsByTextFilterAsync(string textFilter)
        {
            var count = await (from st in _sqlServerDbContext.Set<Student>()
                               where st.Code.Contains(textFilter) || st.Firstname.Contains(textFilter)
                                      || st.Lastname.Contains(textFilter)
                               select st)
                               .CountAsync();

            return count;
        }

        public async Task<List<Student>> SearchStudentsByTextFilterAsync(string textFilter, int currentPage, int pageSize)
        {
            var teachers = await (from st in _sqlServerDbContext.Set<Student>()
                                  where st.Code.Contains(textFilter) || st.Firstname.Contains(textFilter)
                                         || st.Lastname.Contains(textFilter)
                                  orderby st.CreatedAt descending
                                  select new Student
                                  {
                                      Id = st.Id,
                                      Code = st.Code,
                                      Firstname = st.Firstname,
                                      Lastname = st.Lastname,
                                      BirthDate = st.BirthDate,
                                      CreatedAt = st.CreatedAt,
                                      ModifiedAt = st.ModifiedAt
                                  })
                                .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                .Take(Convert.ToInt32(pageSize))
                                .ToListAsync();

            return teachers;
        }

        public async Task<int> TotalCountStudentsByObjectAsync(StudentsQuery studentsQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = "SELECT COUNT(*) FROM Students st ";
            var sqlFilters = "";

            if (studentsQuery.FilteringCriteria != null)
            {
                if (studentsQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = studentsQuery.FilteringCriteria.Code.Operator;
                    var codeValue = studentsQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"st.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.Firstname != null)
                {
                    var firstnameOperator = studentsQuery.FilteringCriteria.Firstname.Operator;
                    var firstnameValue = studentsQuery.FilteringCriteria.Firstname.Value;
                    sqlFilters += @$"st.Firstname 
                                    {ConvertOperatorToSQL(firstnameOperator)} 
                                    {ConvertValueToSQL(firstnameOperator, firstnameValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.Lastname != null)
                {
                    var lastnameOperator = studentsQuery.FilteringCriteria.Lastname.Operator;
                    var lastnameValue = studentsQuery.FilteringCriteria.Lastname.Value;
                    sqlFilters += @$"st.Lastname 
                                    {ConvertOperatorToSQL(lastnameOperator)} 
                                    {ConvertValueToSQL(lastnameOperator, lastnameValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.BirthDate != null)
                {
                    var createdAtOperator = studentsQuery.FilteringCriteria.BirthDate.Operator;
                    var createdAtValue = studentsQuery.FilteringCriteria.BirthDate.Value;
                    sqlFilters += @$"st.BirthDate 
                                    {ConvertOperatorToSQL(createdAtOperator)} 
                                    {ConvertValueToSQL(createdAtOperator, createdAtValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            var result = await connection.ExecuteScalarAsync<int>(sql);

            return result;
        }

        public async Task<IEnumerable<Student>> SearchStudentsByObjectAsync(StudentsPaginatedQuery studentsQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = @"SELECT st.Id, st.Firstname, st.Lastname, st.Code, CONVERT(date, st.BirthDate) as BirthDate, 
                                st.CreatedAt
                        FROM Students st ";
            var sqlFilters = "";
            var sqlOrders = "";
            var sqlCurrentPage = $"OFFSET {studentsQuery.CurrentPage.Value - 1} ROWS ";
            var sqlPageSize = $"FETCH NEXT {studentsQuery.PageSize} ROWS ONLY ";

            if (studentsQuery.FilteringCriteria != null)
            {
                if (studentsQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = studentsQuery.FilteringCriteria.Code.Operator;
                    var codeValue = studentsQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"st.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.Firstname != null)
                {
                    var firstnameOperator = studentsQuery.FilteringCriteria.Firstname.Operator;
                    var firstnameValue = studentsQuery.FilteringCriteria.Firstname.Value;
                    sqlFilters += @$"st.Firstname 
                                    {ConvertOperatorToSQL(firstnameOperator)} 
                                    {ConvertValueToSQL(firstnameOperator, firstnameValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.Lastname != null)
                {
                    var lastnameOperator = studentsQuery.FilteringCriteria.Lastname.Operator;
                    var lastnameValue = studentsQuery.FilteringCriteria.Lastname.Value;
                    sqlFilters += @$"st.Lastname 
                                    {ConvertOperatorToSQL(lastnameOperator)} 
                                    {ConvertValueToSQL(lastnameOperator, lastnameValue)} AND ";
                }

                if (studentsQuery.FilteringCriteria.BirthDate != null)
                {
                    var birthDateOperator = studentsQuery.FilteringCriteria.BirthDate.Operator;
                    var birthDateValue = studentsQuery.FilteringCriteria.BirthDate.Value;
                    sqlFilters += @$"co.BirthDate 
                                    {ConvertOperatorToSQL(birthDateOperator)} 
                                    {ConvertValueToSQL(birthDateOperator, birthDateValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            if (studentsQuery.OrderingCriteria != null)
            {
                if (studentsQuery.OrderingCriteria.Code.HasValue)
                {
                    sqlOrders += $"st.Code {studentsQuery.OrderingCriteria.Code.Value.GetEnumDescription()}, ";
                }
                if (studentsQuery.OrderingCriteria.Firstname.HasValue)
                {
                    sqlOrders += $"st.Firstname {studentsQuery.OrderingCriteria.Firstname.Value.GetEnumDescription()}, ";
                }
                if (studentsQuery.OrderingCriteria.Lastname.HasValue)
                {
                    sqlOrders += $"st.Lastname {studentsQuery.OrderingCriteria.Lastname.Value.GetEnumDescription()}, ";
                }
                if (studentsQuery.OrderingCriteria.BirthDate.HasValue)
                {
                    sqlOrders += $"st.BirthDate {studentsQuery.OrderingCriteria.BirthDate.Value.GetEnumDescription()}, ";
                }
                if (studentsQuery.OrderingCriteria.CreatedAt.HasValue)
                {
                    sqlOrders += $"st.CreatedAt {studentsQuery.OrderingCriteria.CreatedAt.Value.GetEnumDescription()}, ";
                }

                sqlOrders = $"ORDER BY {sqlOrders.TrimEnd(',', ' ')} ";
            }
            else
            {
                sqlOrders = "ORDER BY st.CreatedAt ";
            }

            sql += sqlOrders;
            sql += sqlCurrentPage;
            sql += sqlPageSize;

            var result = await connection.QueryAsync<Student>(sql);

            return result;
        }
    }
}
