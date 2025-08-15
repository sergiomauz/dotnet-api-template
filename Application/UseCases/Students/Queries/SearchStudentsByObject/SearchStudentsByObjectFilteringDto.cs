using System.Text.Json.Serialization;
using Application.Commons.RequestParams;


namespace Application.UseCases.Students.Queries.SearchStudentsByObject
{
    public class SearchStudentsByObjectFilteringDto
    {
        [JsonPropertyName("code")]
        public FilteringCriterionRequestParams? Code { get; set; }

        [JsonPropertyName("firstname")]
        public FilteringCriterionRequestParams? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public FilteringCriterionRequestParams? Lastname { get; set; }

        [JsonPropertyName("birth_date")]
        public FilteringCriterionRequestParams? BirthDate { get; set; }

        [JsonPropertyName("created_at")]
        public FilteringCriterionRequestParams? CreatedAt { get; set; }
    }
}
