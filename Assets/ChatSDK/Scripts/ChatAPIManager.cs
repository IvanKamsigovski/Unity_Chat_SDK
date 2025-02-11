using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChatSDK
{
    public class ChatAPIManager
    {
        #region Get Methodes
        /// <summary>
        /// Used for getting list of all users
        /// </summary>
        public static async Task<List<User>> GetUsers(string url, Action<string> callbaclk = null)
        {
            var users = await WebRequestHandler.GetRequest<List<User>>($"{url}/users");
            callbaclk?.Invoke(JsonConvert.SerializeObject(users));
            return users;
        }

        public static async Task<User> GetUser(string url, int id, Action<string> callbaclk = null)
        {
            User user = await WebRequestHandler.GetRequest<User>($"{url}/users/{id}");
            callbaclk?.Invoke(JsonConvert.SerializeObject(user));
            return user;
        }

        public static async Task<List<Channels>> GetChannels(string url, Action<string> callbaclk = null)
        {
            List<Channels> channels = await WebRequestHandler.GetRequest<List<Channels>>($"{url}/channels");
            callbaclk?.Invoke(JsonConvert.SerializeObject(channels));
            return channels;
        }
        public static async Task<Channels> GetChannel(string url, int id, Action<string> callbaclk = null)
        {
            Channels channel = await WebRequestHandler.GetRequest<Channels>($"{url}/channels/{id}");
            callbaclk?.Invoke(JsonConvert.SerializeObject(channel));
            return channel;
        }

        public static async Task<List<User>> GetChanelUsers(string url, int id, Action<string> callbaclk = null)
        {
            List<User> users = await WebRequestHandler.GetRequest<List<User>>($"{url}/channels/{id}/users");
            callbaclk?.Invoke(JsonConvert.SerializeObject(users));
            return users;
        }
        #endregion

        #region Post Methodes
        public static async Task<UserResponse> CreateUser(string url, User user, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(user);
            var response = await WebRequestHandler.PostRequest<UserResponse>($"{url}/users", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;
        }

        public static async Task<ChannelResponse> CreateChannel(string url, Channels channel, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(channel);
            var response = await WebRequestHandler.PostRequest<ChannelResponse>($"{url}/channels", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;
        }

        public static async Task<ChannelUsersResponse> AddUserToChannel(string url, int userId, User user, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(user);
            var response = await WebRequestHandler.PostRequest<ChannelUsersResponse>($"{url}/channels/{userId}/add-user", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;

        }

        public static async Task<bool> RemoveUser(string url, int userId, User user, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(user);
            var response = await WebRequestHandler.PostRequest($"{url}/channels/{userId}/remove-user", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;
        }
        #endregion

        #region Patch Methodes
        public static async Task<UserResponse> UpdateUser(string url, int userId, User user, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(user);
            var response = await WebRequestHandler.PathchRequest<UserResponse>($"{url}/users/{userId}", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;
        }
        public static async Task<ChannelResponse> UpdateChannel(string url, int id, Channels channel, Action<string> callbaclk = null)
        {
            string message = JsonConvert.SerializeObject(channel);
            var response = await WebRequestHandler.PathchRequest<ChannelResponse>($"{url}/channels/{id}", message);
            callbaclk?.Invoke(JsonConvert.SerializeObject(response));
            return response;
        }
        #endregion

        #region Delete Methodes
        public static async Task<bool> DeleteUser(string url, int userId, Action<string> callbaclk = null)
        {
            var response = await WebRequestHandler.DeleteRequest($"{url}/users/{userId}");
            callbaclk?.Invoke(JsonConvert.SerializeObject(response.ToString()));
            return response;
        }
        public static async Task<bool> DeleteChannel(string url, int id, Action<string> callbaclk = null)
        {
            var response = await WebRequestHandler.DeleteRequest($"{url}/channels/{id}");
            callbaclk?.Invoke(JsonConvert.SerializeObject(response.ToString()));
            return response;
        }
        #endregion
    }

}

