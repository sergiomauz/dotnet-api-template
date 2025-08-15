using Commons.Enums;
using Domain.QueryObjects.Utils;


namespace Domain.QueryObjects
{
    public class CoursesQueryFilter
    {
        public FilteringCriterion? Code { get; set; }
        public FilteringCriterion? Name { get; set; }
        public FilteringCriterion? Description { get; set; }
        public FilteringCriterion? CreatedAt { get; set; }
    }

    public class CoursesQueryOrder
    {
        public OrderOperator? Code { get; set; }
        public OrderOperator? Name { get; set; }
        public OrderOperator? Description { get; set; }
        public OrderOperator? CreatedAt { get; set; }
    }

    public class CoursesQuery : QueryTemplate<CoursesQueryFilter, CoursesQueryOrder>
    {
    }

    public class CoursesPaginatedQuery : PaginatedQueryTemplate<CoursesQueryFilter, CoursesQueryOrder>
    {
    }
}
