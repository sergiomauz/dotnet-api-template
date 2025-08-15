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
    public class CoursesRepository : BaseWithCodeRepository<Course>, ICoursesRepository
    {
        private readonly SqlServerDbContext _sqlServerDbContext;

        public CoursesRepository(SqlServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<int> TotalCoursesByTeacherIdAsync(int teacherId)
        {
            var count = await (from co in _sqlServerDbContext.Set<Course>()
                               where co.TeacherId == teacherId
                               select co)
                              .CountAsync();

            return count;
        }

        public async Task<List<Course>> GetCoursesByTeacherIdAsync(int teacherId, int currentPage, int pageSize)
        {
            var courses = await (from en in _sqlServerDbContext.Set<Enrollment>()
                                 join co in _sqlServerDbContext.Set<Course>() on en.CourseId equals co.Id into leftJoinCO
                                 from enco in leftJoinCO.DefaultIfEmpty()
                                 where enco.TeacherId == teacherId
                                 group new { en, enco } by new
                                 {
                                     enco.Id,
                                     enco.Code,
                                     enco.Name,
                                     enco.CreatedAt
                                 } into g
                                 orderby g.Key.CreatedAt descending
                                 select new Course
                                 {
                                     Id = g.Key.Id,
                                     Code = g.Key.Code,
                                     Name = g.Key.Name,
                                     NotMappedStudents = g.Count()
                                 })
                                .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                .Take(Convert.ToInt32(pageSize))
                                .ToListAsync();

            return courses;
        }

        public async Task<int> TotalCountCoursesByTextFilterAsync(string textFilter)
        {
            var count = await (from co in _sqlServerDbContext.Set<Course>()
                               where co.Code.Contains(textFilter) || co.Name.Contains(textFilter)
                                      || co.Description.Contains(textFilter)
                               select co)
                               .CountAsync();

            return count;
        }

        public async Task<List<Course>> SearchCoursesByTextFilterAsync(string textFilter, int currentPage, int pageSize)
        {
            var courses = await (from co in _sqlServerDbContext.Set<Course>()
                                 where co.Code.Contains(textFilter) || co.Name.Contains(textFilter)
                                        || co.Description.Contains(textFilter)
                                 orderby co.CreatedAt descending
                                 select new Course
                                 {
                                     Id = co.Id,
                                     Code = co.Code,
                                     Name = co.Name,
                                     Description = co.Description,
                                     CreatedAt = co.CreatedAt,
                                     ModifiedAt = co.ModifiedAt
                                 })
                                .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                .Take(Convert.ToInt32(pageSize))
                                .ToListAsync();

            return courses;
        }

        public async Task<int> TotalCountCoursesByObjectAsync(CoursesQuery coursesQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = "SELECT COUNT(*) FROM Courses co ";
            var sqlFilters = "";

            if (coursesQuery.FilteringCriteria != null)
            {
                if (coursesQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = coursesQuery.FilteringCriteria.Code.Operator;
                    var codeValue = coursesQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"co.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.Name != null)
                {
                    var nameOperator = coursesQuery.FilteringCriteria.Name.Operator;
                    var nameValue = coursesQuery.FilteringCriteria.Name.Value;
                    sqlFilters += @$"co.Name 
                                    {ConvertOperatorToSQL(nameOperator)} 
                                    {ConvertValueToSQL(nameOperator, nameValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.Description != null)
                {
                    var descriptionOperator = coursesQuery.FilteringCriteria.Description.Operator;
                    var descriptionValue = coursesQuery.FilteringCriteria.Description.Value;
                    sqlFilters += @$"co.Description 
                                    {ConvertOperatorToSQL(descriptionOperator)} 
                                    {ConvertValueToSQL(descriptionOperator, descriptionValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.CreatedAt != null)
                {
                    var createdAtOperator = coursesQuery.FilteringCriteria.CreatedAt.Operator;
                    var createdAtValue = coursesQuery.FilteringCriteria.CreatedAt.Value;
                    sqlFilters += @$"co.CreatedAt 
                                    {ConvertOperatorToSQL(createdAtOperator)} 
                                    {ConvertValueToSQL(createdAtOperator, createdAtValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            var result = await connection.ExecuteScalarAsync<int>(sql);

            return result;
        }

        public async Task<IEnumerable<Course>> SearchCoursesByObjectAsync(CoursesPaginatedQuery coursesQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = "SELECT * FROM Courses co ";
            var sqlFilters = "";
            var sqlOrders = "";
            var sqlCurrentPage = $"OFFSET {coursesQuery.CurrentPage.Value - 1} ROWS ";
            var sqlPageSize = $"FETCH NEXT {coursesQuery.PageSize} ROWS ONLY ";

            if (coursesQuery.FilteringCriteria != null)
            {
                if (coursesQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = coursesQuery.FilteringCriteria.Code.Operator;
                    var codeValue = coursesQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"co.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.Name != null)
                {
                    var nameOperator = coursesQuery.FilteringCriteria.Name.Operator;
                    var nameValue = coursesQuery.FilteringCriteria.Name.Value;
                    sqlFilters += @$"co.Name 
                                    {ConvertOperatorToSQL(nameOperator)} 
                                    {ConvertValueToSQL(nameOperator, nameValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.Description != null)
                {
                    var descriptionOperator = coursesQuery.FilteringCriteria.Description.Operator;
                    var descriptionValue = coursesQuery.FilteringCriteria.Description.Value;
                    sqlFilters += @$"co.Description 
                                    {ConvertOperatorToSQL(descriptionOperator)} 
                                    {ConvertValueToSQL(descriptionOperator, descriptionValue)} AND ";
                }

                if (coursesQuery.FilteringCriteria.CreatedAt != null)
                {
                    var createdAtOperator = coursesQuery.FilteringCriteria.CreatedAt.Operator;
                    var createdAtValue = coursesQuery.FilteringCriteria.CreatedAt.Value;
                    sqlFilters += @$"co.CreatedAt 
                                    {ConvertOperatorToSQL(createdAtOperator)} 
                                    {ConvertValueToSQL(createdAtOperator, createdAtValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            if (coursesQuery.OrderingCriteria != null)
            {
                if (coursesQuery.OrderingCriteria.Code.HasValue)
                {
                    sqlOrders += $"co.Code {coursesQuery.OrderingCriteria.Code.Value.GetEnumDescription()}, ";
                }
                if (coursesQuery.OrderingCriteria.Name.HasValue)
                {
                    sqlOrders += $"co.Name {coursesQuery.OrderingCriteria.Name.Value.GetEnumDescription()}, ";
                }
                if (coursesQuery.OrderingCriteria.Description.HasValue)
                {
                    sqlOrders += $"co.Description {coursesQuery.OrderingCriteria.Description.Value.GetEnumDescription()}, ";
                }
                if (coursesQuery.OrderingCriteria.CreatedAt.HasValue)
                {
                    sqlOrders += $"co.CreatedAt {coursesQuery.OrderingCriteria.CreatedAt.Value.GetEnumDescription()}, ";
                }
                sqlOrders = $"ORDER BY {sqlOrders.TrimEnd(',', ' ')} ";
            }
            else
            {
                sqlOrders = "ORDER BY co.CreatedAt ";
            }

            sql += sqlOrders;
            sql += sqlCurrentPage;
            sql += sqlPageSize;

            var result = await connection.QueryAsync<Course>(sql);

            return result;
        }
    }
}
