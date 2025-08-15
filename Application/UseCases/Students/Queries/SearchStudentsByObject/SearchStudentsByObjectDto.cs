using Application.Commons.RequestParams;


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectDto :
        ObjectRequestParams<SearchStudentsByObjectFilteringDto, SearchStudentsByObjectOrderingDto>
    {
    }
}
