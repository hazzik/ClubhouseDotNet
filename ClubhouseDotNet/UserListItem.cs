﻿using System;
using System.Text.Json.Serialization;

namespace ClubhouseDotNet
{
    public class UserListItem
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("photo_url")]
        public Uri PhotoUrl { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("last_active_minutes")]
        public long LastActiveMinutes { get; set; }

        [JsonPropertyName("bio")]
        public string Bio { get; set; }

        [JsonPropertyName("twitter")]
        public string Twitter { get; set; }
    }
}