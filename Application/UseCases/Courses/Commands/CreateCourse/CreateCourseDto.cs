using System.Text.Json.Serialization;


namespace Application.UseCases.Courses.Commands.CreateCourse
{
    public class CreateCourseDto
    {
        [JsonPropertyName("teacher_id")]
        public int? TeacherId { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
