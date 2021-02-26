using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public partial class GetActionableNotificationsResponse : ClubhouseResponse
    {
        [JsonPropertyName("notifications")]
        public object[] Notifications { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("next")]
        public object Next { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }
    }
}