using Commons.Enums;
using Domain.QueryObjects.Utils;


namespace Domain.QueryObjects
{
    public class TeachersQueryFilter
    {
        public FilteringCriterion? Code { get; set; }
        public FilteringCriterion? Firstname { get; set; }
        public FilteringCriterion? Lastname { get; set; }
        public FilteringCriterion? CreatedAt { get; set; }
    }

    public class TeachersQueryOrder
    {
        public OrderOperator? Code { get; set; }
        public OrderOperator? Firstname { get; set; }
        public OrderOperator? Lastname { get; set; }
        public OrderOperator? CreatedAt { get; set; }
    }

    public class TeachersQuery : QueryTemplate<TeachersQueryFilter, TeachersQueryOrder>
    {
    }

    public class TeachersPaginatedQuery : PaginatedQueryTemplate<TeachersQueryFilter, TeachersQueryOrder>
    {
    }
}
