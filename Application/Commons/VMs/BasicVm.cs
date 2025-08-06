using System.Text.Json.Serialization;


namespace Application.Commons.VMs
{
    public class BasicVm
    {
        [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }

        [JsonPropertyName("created_at"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CreatedAt { get; set; }

        [JsonPropertyName("modified_at"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ModifiedAt { get; set; }
    }
}
