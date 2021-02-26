using System;

namespace ClubhouseDotNet
{
    public interface IPubnubConfig
    {
        Guid PubnubToken { get; set; }
        string PubnubOrigin { get; set; }
        int PubnubHeartbeatValue { get; set; }
        int PubnubHeartbeatInterval { get; set; }
    }
}