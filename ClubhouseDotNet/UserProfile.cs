using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public class UserProfile
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}