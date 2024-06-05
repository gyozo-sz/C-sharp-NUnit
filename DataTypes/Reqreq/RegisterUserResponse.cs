using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class RegisterUserResponse : ApiData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
