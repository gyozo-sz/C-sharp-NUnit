using Newtonsoft.Json;

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

        public override int GetHashCode()
        {
            return (FullName + Email + PermanentAddress + CurrentAddress).GetHashCode();
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as TextBoxData);
        }
    }
}
