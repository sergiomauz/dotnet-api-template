using System.Data;
using System.Data.Common;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Commons.Enums;
using Domain.Entities.Bases;
using Application.Infrastructure.Persistence.Bases;


namespace Persistence.Repositories.Bases
{
    public abstract class BaseWithIdRepository<T> : IBaseWithIdRepository<T> where T : BaseEntityWithId
    {
        private readonly SqlServerDbContext _sqlServerDbContext;

        public BaseWithIdRepository(SqlServerDbContext sqlServerDbContext)
        {
            _sqlServerDbContext = sqlServerDbContext;
        }

        public async Task<DbConnection> EnsureConnectionOpenAsync(DbContext dbContext)
        {
            var connection = dbContext.Database.GetDbConnection();
            if (dbContext.Database.GetDbConnection().State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return connection;
        }

        public string ConvertOperatorToSQL(FilterOperator op)
        {
            if (op == FilterOperator.Contains || op == FilterOperator.StartsWith || op == FilterOperator.EndsWith)
            {
                return "LIKE";
            }

            return op.GetEnumDescription();
        }

        public string ConvertValueToSQL(FilterOperator op, object? value)
        {
            if (value == null)
            {
                return "NULL";
            }

            if (value is JsonElement json)
            {
                if (json.ValueKind == JsonValueKind.String)
                {
                    var str = json.GetString();

                    // For datetime
                    if (DateTime.TryParse(str, out DateTime dt))
                    {
                        return $"'{dt:yyyy-MM-dd HH:mm:ss.fffffff}'";
                    }

                    // For normal strings
                    if (op == FilterOperator.Contains)
                    {
                        return $"'%{str?.Replace("'", "''")}%'";
                    }
                    else if (op == FilterOperator.StartsWith)
                    {
                        return $"'{str?.Replace("'", "''")}%'";
                    }
                    else if (op == FilterOperator.EndsWith)
                    {
                        return $"'%{str?.Replace("'", "''")}'";
                    }

                    return $"'{str?.Replace("'", "''")}'";
                }

                // For number
                if (json.ValueKind == JsonValueKind.Number)
                {
                    return json.GetRawText();
                }

                // For booleans
                if (json.ValueKind == JsonValueKind.True || json.ValueKind == JsonValueKind.False)
                {
                    return json.GetBoolean() ? "1" : "0";
                }

                // For arrays
                if (json.ValueKind == JsonValueKind.Array)
                {
                    var items = json.EnumerateArray().Select(element => ConvertValueToSQL(FilterOperator.Equals, element));

                    return $"({string.Join(", ", items)})";
                }
            }

            return value.ToString() ?? "NULL";
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {
            if (entity is BaseEntityWithId tracked)
            {
                tracked.CreatedAt = DateTime.UtcNow;
            }
            var entry = await _sqlServerDbContext.Set<T>().AddAsync(entity);
            await _sqlServerDbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            var affectedRows = 0;
            var entity = await _sqlServerDbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _sqlServerDbContext.Set<T>().Remove(entity);
                affectedRows = await _sqlServerDbContext.SaveChangesAsync();
            }

            return affectedRows;
        }

        public virtual async Task<int> DeleteAsync(List<int> ids)
        {
            var entities = await _sqlServerDbContext.Set<T>().Where(e => ids.Contains(e.Id.Value)).ToListAsync();
            _sqlServerDbContext.Set<T>().RemoveRange(entities);
            var affectedRows = await _sqlServerDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var existingEntity = await _sqlServerDbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == entity.Id);
            if (existingEntity == null)
                return null;

            if (entity is BaseEntityWithId tracked)
            {
                tracked.ModifiedAt = DateTime.UtcNow;
            }
            _sqlServerDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _sqlServerDbContext.SaveChangesAsync();

            return existingEntity;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _sqlServerDbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == id);

            return entity;
        }
    }
}
