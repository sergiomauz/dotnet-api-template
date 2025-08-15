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
    public class TeachersRepository : BaseWithCodeRepository<Teacher>, ITeachersRepository
    {
        private readonly SqlServerDbContext _sqlServerDbContext;

        public TeachersRepository(SqlServerDbContext sqlServerDbContext) : base(sqlServerDbContext)
        {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<int> TotalCountTeachersByTextFilterAsync(string textFilter)
        {
            var count = await (from te in _sqlServerDbContext.Set<Teacher>()
                               where te.Code.Contains(textFilter) || te.Firstname.Contains(textFilter)
                                      || te.Lastname.Contains(textFilter)
                               select te)
                               .CountAsync();

            return count;
        }

        public async Task<List<Teacher>> SearchTeachersByTextFilterAsync(string textFilter, int currentPage, int pageSize)
        {
            var teachers = await (from te in _sqlServerDbContext.Set<Teacher>()
                                  where te.Code.Contains(textFilter) || te.Firstname.Contains(textFilter)
                                         || te.Lastname.Contains(textFilter)
                                  orderby te.CreatedAt descending
                                  select new Teacher
                                  {
                                      Id = te.Id,
                                      Code = te.Code,
                                      Firstname = te.Firstname,
                                      Lastname = te.Lastname,
                                      CreatedAt = te.CreatedAt,
                                      ModifiedAt = te.ModifiedAt
                                  })
                                   .Skip(Convert.ToInt32(pageSize) * (Convert.ToInt32(currentPage) - 1))
                                   .Take(Convert.ToInt32(pageSize))
                                   .ToListAsync();

            return teachers;
        }

        public async Task<int> TotalCountTeachersByObjectAsync(TeachersQuery teachersQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = "SELECT COUNT(*) FROM Teachers te ";
            var sqlFilters = "";

            if (teachersQuery.FilteringCriteria != null)
            {
                if (teachersQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = teachersQuery.FilteringCriteria.Code.Operator;
                    var codeValue = teachersQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"te.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (teachersQuery.FilteringCriteria.Firstname != null)
                {
                    var firstnameOperator = teachersQuery.FilteringCriteria.Firstname.Operator;
                    var firstnameValue = teachersQuery.FilteringCriteria.Firstname.Value;
                    sqlFilters += @$"te.Firstname 
                                    {ConvertOperatorToSQL(firstnameOperator)} 
                                    {ConvertValueToSQL(firstnameOperator, firstnameValue)} AND ";
                }

                if (teachersQuery.FilteringCriteria.Lastname != null)
                {
                    var lastnameOperator = teachersQuery.FilteringCriteria.Lastname.Operator;
                    var lastnameValue = teachersQuery.FilteringCriteria.Lastname.Value;
                    sqlFilters += @$"te.Lastname 
                                    {ConvertOperatorToSQL(lastnameOperator)} 
                                    {ConvertValueToSQL(lastnameOperator, lastnameValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            var result = await connection.ExecuteScalarAsync<int>(sql);

            return result;
        }

        public async Task<IEnumerable<Teacher>> SearchTeachersByObjectAsync(TeachersPaginatedQuery teachersQuery)
        {
            var connection = await EnsureConnectionOpenAsync(_sqlServerDbContext);
            var sql = "SELECT * FROM Teachers te ";
            var sqlFilters = "";
            var sqlOrders = "";
            var sqlCurrentPage = $"OFFSET {teachersQuery.CurrentPage.Value - 1} ROWS ";
            var sqlPageSize = $"FETCH NEXT {teachersQuery.PageSize} ROWS ONLY ";

            if (teachersQuery.FilteringCriteria != null)
            {
                if (teachersQuery.FilteringCriteria.Code != null)
                {
                    var codeOperator = teachersQuery.FilteringCriteria.Code.Operator;
                    var codeValue = teachersQuery.FilteringCriteria.Code.Value;
                    sqlFilters += @$"te.Code 
                                    {ConvertOperatorToSQL(codeOperator)} 
                                    {ConvertValueToSQL(codeOperator, codeValue)} AND ";
                }

                if (teachersQuery.FilteringCriteria.Firstname != null)
                {
                    var firstnameOperator = teachersQuery.FilteringCriteria.Firstname.Operator;
                    var firstnameValue = teachersQuery.FilteringCriteria.Firstname.Value;
                    sqlFilters += @$"te.Firstname 
                                    {ConvertOperatorToSQL(firstnameOperator)} 
                                    {ConvertValueToSQL(firstnameOperator, firstnameValue)} AND ";
                }

                if (teachersQuery.FilteringCriteria.Lastname != null)
                {
                    var lastnameOperator = teachersQuery.FilteringCriteria.Lastname.Operator;
                    var lastnameValue = teachersQuery.FilteringCriteria.Lastname.Value;
                    sqlFilters += @$"te.Lastname 
                                    {ConvertOperatorToSQL(lastnameOperator)} 
                                    {ConvertValueToSQL(lastnameOperator, lastnameValue)} AND ";
                }

                sqlFilters = $"WHERE {sqlFilters.Substring(0, sqlFilters.Length - 5)} ";

                sql += sqlFilters;
            }

            if (teachersQuery.OrderingCriteria != null)
            {
                if (teachersQuery.OrderingCriteria.Code.HasValue)
                {
                    sqlOrders += $"te.Code {teachersQuery.OrderingCriteria.Code.Value.GetEnumDescription()}, ";
                }
                if (teachersQuery.OrderingCriteria.Firstname.HasValue)
                {
                    sqlOrders += $"te.Firstname {teachersQuery.OrderingCriteria.Firstname.Value.GetEnumDescription()}, ";
                }
                if (teachersQuery.OrderingCriteria.Lastname.HasValue)
                {
                    sqlOrders += $"te.Lastname {teachersQuery.OrderingCriteria.Lastname.Value.GetEnumDescription()}, ";
                }
                if (teachersQuery.OrderingCriteria.CreatedAt.HasValue)
                {
                    sqlOrders += $"te.CreatedAt {teachersQuery.OrderingCriteria.CreatedAt.Value.GetEnumDescription()}, ";
                }
                sqlOrders = $"ORDER BY {sqlOrders.TrimEnd(',', ' ')} ";
            }
            else
            {
                sqlOrders = "ORDER BY te.CreatedAt ";
            }

            sql += sqlOrders;
            sql += sqlCurrentPage;
            sql += sqlPageSize;

            var result = await connection.QueryAsync<Teacher>(sql);

            return result;
        }
    }
}
