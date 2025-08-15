using Application.Commons.RequestParams;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectDto :
        ObjectRequestParams<SearchTeachersByObjectFilteringDto, SearchTeachersByObjectOrderingDto>
    {
    }
}
