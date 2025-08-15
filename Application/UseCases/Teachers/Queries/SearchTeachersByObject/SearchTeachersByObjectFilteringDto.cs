using System.Text.Json.Serialization;
using Application.Commons.RequestParams;


namespace Application.UseCases.Teachers.Queries.SearchTeachersByObject
{
    public class SearchTeachersByObjectFilteringDto
    {
        [JsonPropertyName("code")]
        public FilteringCriterionRequestParams? Code { get; set; }

        [JsonPropertyName("firstname")]
        public FilteringCriterionRequestParams? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public FilteringCriterionRequestParams? Lastname { get; set; }

        [JsonPropertyName("created_at")]
        public FilteringCriterionRequestParams? CreatedAt { get; set; }
    }
}
