using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class ErrorResponse : ApiData
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
