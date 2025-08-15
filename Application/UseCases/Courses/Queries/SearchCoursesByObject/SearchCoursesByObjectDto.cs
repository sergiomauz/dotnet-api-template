using Application.Commons.RequestParams;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectDto :
        ObjectRequestParams<SearchCoursesByObjectFilteringDto, SearchCoursesByObjectOrderingDto>
    {
    }
}
