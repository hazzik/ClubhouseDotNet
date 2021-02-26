using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public partial class InviteToNewChannelResponse : ClubhouseResponse
    {
        [JsonPropertyName("channel_invite_id")]
        public long ChannelInviteId { get; set; }
    }
}