using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public class ChannelList : ClubhouseResponse
    {
        [JsonPropertyName("channels")]
        public Channel[] Channels { get; set; }

        [JsonPropertyName("events")]
        public ChannelEvents[] Events { get; set; }
    }
}