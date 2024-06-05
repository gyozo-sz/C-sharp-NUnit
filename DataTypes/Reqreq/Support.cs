using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class Support : ApiData
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
