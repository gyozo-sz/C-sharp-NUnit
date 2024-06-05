using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    internal class LoginUserData : ApiData
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
