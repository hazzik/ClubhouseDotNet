using System;
using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public class AcceptSpeakerInviteResponse : ClubhouseResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("pubnub_token")]
        public Guid PubnubToken { get; set; }

        [JsonPropertyName("pubnub_origin")]
        public string PubnubOrigin { get; set; }

        [JsonPropertyName("pubnub_heartbeat_value")]
        public long PubnubHeartbeatValue { get; set; }

        [JsonPropertyName("pubnub_heartbeat_interval")]
        public long PubnubHeartbeatInterval { get; set; }
    }
}