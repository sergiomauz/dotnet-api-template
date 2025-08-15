using Microsoft.AspNetCore.Mvc;


namespace Application.UseCases.Teachers.Queries.GetTeacherByCode
{
    public class GetTeacherByCodeRoute
    {
        [FromRoute(Name = "code")]
        public string? Code { get; set; }
    }
}
