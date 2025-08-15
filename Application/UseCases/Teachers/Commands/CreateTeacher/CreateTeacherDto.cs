using System.Text.Json.Serialization;


namespace Application.UseCases.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }
    }
}
