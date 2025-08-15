using System.Text.Json.Serialization;


namespace Application.UseCases.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }
    }
}
