using Commons.Enums;
using Domain.QueryObjects.Utils;


namespace Domain.QueryObjects
{
    public class StudentsQueryFilter
    {
        public FilteringCriterion? Code { get; set; }
        public FilteringCriterion? Firstname { get; set; }
        public FilteringCriterion? Lastname { get; set; }
        public FilteringCriterion? BirthDate { get; set; }
        public FilteringCriterion? CreatedAt { get; set; }
    }

    public class StudentsQueryOrder
    {
        public OrderOperator? Code { get; set; }
        public OrderOperator? Firstname { get; set; }
        public OrderOperator? Lastname { get; set; }
        public OrderOperator? BirthDate { get; set; }
        public OrderOperator? CreatedAt { get; set; }
    }

    public class StudentsQuery : QueryTemplate<StudentsQueryFilter, StudentsQueryOrder>
    {
    }

    public class StudentsPaginatedQuery : PaginatedQueryTemplate<StudentsQueryFilter, StudentsQueryOrder>
    {
    }
}
