using System.Text.Json;
using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class GetRequestResponse<DataType> : ApiData
    {
        [JsonPropertyName("data")]
        public DataType Data { get; set; }

        [JsonPropertyName("support")]
        public Support Support { get; set; }
    }
}
