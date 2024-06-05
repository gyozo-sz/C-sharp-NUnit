using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NUnit_practice.DataTypes
{
    public class CreateUserResponse : CreateUserData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
