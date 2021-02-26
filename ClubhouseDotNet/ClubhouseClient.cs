﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClubhouseDotNet
{
    public class ClubhouseClient 
    {
        private readonly HttpClient _client;

        public ClubhouseClient(string authToken)
        {
            _client = new HttpClient(new AuthenticatedHttpClientHandler(
                () => Task.FromResult(authToken),
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                }))
            {
                BaseAddress = new Uri("https://www.clubhouseapi.com/")
            };

            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Accept-Language", "en-JP;q=1, ja-JP;q=0.9");

            _client.DefaultRequestHeaders.Add(HeaderNames.ChLanguages, "en-JP,ja-JP");
            _client.DefaultRequestHeaders.Add(HeaderNames.ChLocale, "en-JP");
            _client.DefaultRequestHeaders.Add(HeaderNames.ChAppBuild, Constants.API_BUILD_ID);
            _client.DefaultRequestHeaders.Add(HeaderNames.ChAppVersion, Constants.API_BUILD_VERSION);
            _client.DefaultRequestHeaders.Add("User-Agent", Constants.API_UA);
            _client.DefaultRequestHeaders.Add("Connection", "close");
            //_client.DefaultRequestHeaders.Add("Cookie", $"__cfduid={secrets.token_hex(21)}{random.randint(1, 9)}");
        }

        public async Task<MeResponse> Me(bool returnFollowingIds=false, bool returnBlockedIds=false, string timezoneIdentifier="Asia/Tokyo")
        {
            var response = await _client.PostAsJsonAsync("/api/me", new MeRequest {ReturnFollowingIds = returnFollowingIds, ReturnBlockedIds = returnBlockedIds, TimezoneIdentifier = timezoneIdentifier});

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<MeResponse>();
        }

        public async Task<ChannelList> GetChannelsAsync()
        {
            var response = await _client.GetAsync("/api/get_channels");

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ChannelList>();
        }

        public async Task<GetChannelResponse> GetChannelAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/get_channel", new ChannelRequest()
            {
                Name = name,
                ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<GetChannelResponse>();
        }

        public async Task<CreateChannelResponse> CreateChannelAsync(string topic = null, bool isSocialMode = false, bool isPrivate = false, long? clubId = null, long[] userIds = null, long? eventId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/create_channel", new CreateChannelRequest
            {
                IsSocialMode = isSocialMode,
                IsPrivate = isPrivate,
                ClubId = clubId,
                UserIds = userIds ?? Array.Empty<long>(),
                EventId = eventId,
                Topic = topic
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<CreateChannelResponse>();
        }

        public async Task<JoinChannelResponse> JoinChannelAsync(string name, string attributionSource = "feed", string attributionDetails="eyJpc19leHBsb3JlIjpmYWxzZSwicmFuayI6MX0=")
        {
            var response = await _client.PostAsJsonAsync("/api/join_channel", new JoinChannelRequest
            {
                Name = name,
                AttributionSource = attributionSource,
                AttributionDetails = attributionDetails
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<JoinChannelResponse>();
        }

        public async Task<ClubhouseResponse> LeaveChannelAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/leave_channel", new ChannelRequest
            {
                Name = name, ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                    .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }
        
        public async Task<ClubhouseResponse> MakeChannelPublicAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/make_channel_public", new ChannelRequest
            {
                Name = name, ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                    .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }
        
        public async Task<ClubhouseResponse> MakeChannelSocialAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/make_channel_social", new ChannelRequest
            {
                Name = name, ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                    .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }
        
        public async Task<ClubhouseResponse> EndChannelAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/end_channel", new ChannelRequest
            {
                Name = name, 
                ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                    .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ActivePingResponse> ActivePingAsync(string name, long? channelId = null)
        {
            var response = await _client.PostAsJsonAsync("/api/active_ping", new ChannelRequest
            {
                Name = name,
                ChannelId = channelId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ActivePingResponse>();
        }

        public async Task<ClubhouseResponse> InviteToExistingChannelAsync(string name, long userId )
        {
            var response = await _client.PostAsJsonAsync("/api/invite_to_existing_channel", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<InviteToExistingChannelResponse>();
        }

        public async Task<ClubhouseResponse> InviteToNewChannelAsync(string name, long userId )
        {
            var response = await _client.PostAsJsonAsync("/api/invite_to_new_channel", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<InviteToNewChannelResponse>();
        }

        public async Task<ClubhouseResponse> InviteSpeakerAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/invite_speaker", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> UnInviteSpeakerAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/uninvite_speaker", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> MuteSpeakerAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/mute_speaker", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }
        
        public async Task<ClubhouseResponse> MakeModeratorAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/make_moderator", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<AcceptSpeakerInviteResponse> AcceptSpeakerInviteAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/accept_speaker_invite", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<AcceptSpeakerInviteResponse>();
        }

        public async Task<ClubhouseResponse> AudienceReplyAsync(string name, bool raiseHands)
        {
            var response = await _client.PostAsJsonAsync("/api/audience_reply", new 
            {
                channel = name,
                raise_hands = raiseHands,
                unraise_hands = !raiseHands
            });
            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> RejectSpeakerInviteAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/reject_speaker_invite", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> BlockFromChannelAsync(string name, long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/block_from_channel", new 
            {
                channel = name,
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> FollowAsync(long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/follow", new
            {
                source = 4,
                source_topic_id = default(long?),
                user_ids = Array.Empty<long>(),
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> UnFollowAsync(long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/unfollow", new
            {
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> BlockAsync(long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/block", new
            {
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> UnBlockAsync(long userId)
        {
            var response = await _client.PostAsJsonAsync("/api/unblock", new
            {
                user_id = userId
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<ClubhouseResponse>();
        }

        public async Task<ClubhouseResponse> GetActionableNotificationsAsync()
        {
            var response = await _client.GetAsync("/api/get_actionable_notifications");

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<GetActionableNotificationsResponse>();
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var response = await _client.PostAsJsonAsync("/api/refresh_token", new
            {
                refresh = refreshToken
            });

            return await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<RefreshTokenResponse>();
        }
    }
}