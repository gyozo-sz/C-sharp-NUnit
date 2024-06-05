using System.Text.Json.Serialization;

namespace NUnit_practice.DataTypes
{
    public class UpdateUserResponse : CreateUserData
    {
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
