using Microsoft.AspNetCore.Mvc;


namespace Application.UseCases.Students.Queries.GetStudentByCode
{
    public class GetStudentByCodeRoute
    {
        [FromRoute(Name = "code")]
        public string? Code { get; set; }
    }
}
