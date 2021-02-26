using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public class StartPhoneNumberAuthRequest
    {
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
    }
}