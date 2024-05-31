using System.Text.Json;
using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class ResourceList<Resource>
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("data")]
        public List<Resource> Data { get; set; }

        [JsonPropertyName("support")]
        public Support Support { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
