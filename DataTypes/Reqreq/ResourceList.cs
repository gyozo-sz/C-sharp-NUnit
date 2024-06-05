using System.Text.Json;
using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class ResourceList<Resource> : GetRequestResponse<List<Resource>> 
        where Resource : class
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

    }
}
