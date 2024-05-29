using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice.DataClasses
{
    internal class TextBoxData : IEquatable<TextBoxData>
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("currentAddress")]
        public string CurrentAddress { get; set; }

        [JsonProperty("permanentAddress")]
        public string PermanentAddress { get; set; }

        public TextBoxData()
        {
            FullName = "";
            Email = "";
            CurrentAddress = "";
            PermanentAddress = "";
        }

        public bool Equals(TextBoxData? other)
        {
            if (other == null)
                return false;

            if (this.FullName != other.FullName 
                || this.Email != other.Email 
                || this.CurrentAddress != other.CurrentAddress 
                || this.PermanentAddress != other.PermanentAddress)
            {
                return false;
            }
                
            return true;
        }
    }
}
