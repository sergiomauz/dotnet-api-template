using System.Text.Json.Serialization;
using Application.Commons.RequestParams;


namespace Application.UseCases.Courses.Queries.SearchCoursesByObject
{
    public class SearchCoursesByObjectFilteringDto
    {
        [JsonPropertyName("code")]
        public FilteringCriterionRequestParams? Code { get; set; }

        [JsonPropertyName("name")]
        public FilteringCriterionRequestParams? Name { get; set; }

        [JsonPropertyName("description")]
        public FilteringCriterionRequestParams? Description { get; set; }

        [JsonPropertyName("created_at")]
        public FilteringCriterionRequestParams? CreatedAt { get; set; }
    }
}
