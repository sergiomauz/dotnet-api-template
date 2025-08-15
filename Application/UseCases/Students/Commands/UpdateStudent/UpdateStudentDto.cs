using System.Text.Json.Serialization;


namespace Application.UseCases.Students.Commands.UpdateStudent
{
    public class UpdateStudentDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }

        [JsonPropertyName("birth_date")]
        public string? BirthDate { get; set; }
    }
}
