using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Compilation;
using UnityEngine;

public class ChatAPIManager : MonoBehaviour
{
    #region Get Methodes
    /// <summary>
    /// Used for getting list of all users
    /// </summary>
    public async Task<List<User>> GetUsers(string url)
    {
        List<User> users = await WebRequestHandler.GetRequest<List<User>>($"{url}/users");
        return users;
    }
    public async Task<User> GetUser(string url, string id)
    {
        User users = await WebRequestHandler.GetRequest<User>($"{url}/users/{id}");
        return users;
    }

    public async Task<List<Channels>> GetChannels(string url)
    {
        List<Channels> channels = await WebRequestHandler.GetRequest<List<Channels>>($"{url}/channels");
        return channels;
    }
    public async Task<Channels> GetChannel(string url, string id)
    {
        Channels channel = await WebRequestHandler.GetRequest<Channels>($"{url}/channels/{id}");
        return channel;
    }

    public async Task<List<User>> GetChanelUsers(string url, string id)
    {
        List<User> users = await WebRequestHandler.GetRequest<List<User>>($"{url}/channels/{id}/users");
        return users;
    }
    #endregion

    #region Post Methodes
    public async Task<UserResponse> CreateUser(string url, User user)
    {
        string message = JsonUtility.ToJson(user);
        var response = await WebRequestHandler.PostRequest<UserResponse>($"{url}/users", message);
        return response;
    }

    public async Task<ChannelResponse> CreateChannel(string url, Channels channel)
    {
        string message = JsonUtility.ToJson(channel);
        var response = await WebRequestHandler.PostRequest<ChannelResponse>($"{url}/channels", message);
        return response;
    }

    public async Task<ChannelUsersResponse> AddUserToChannel(string url, string userId, User user)
    {
        string message = JsonUtility.ToJson(user);
        var response = await WebRequestHandler.PostRequest<ChannelUsersResponse>($"{url}/channels/{userId}/add-user", message);
        return response;
        
    }

    public async Task<bool> RemoveUser(string url, string userId, User user)
    {
        string message = JsonUtility.ToJson(user);
        var response = await WebRequestHandler.PostRequest($"{url}/channels/{userId}/remove-user", message);
        return response;
    }
    #endregion

    #region Patch Methodes
    public async Task<UserResponse> UpdateUser(string url, string userId, User user)
    {
        string message = JsonUtility.ToJson(user);
        var response = await WebRequestHandler.PathchRequest<UserResponse>($"{url}/users/{userId}", message);
        return response;
    }
    public async Task<ChannelResponse> UpdateChannel(string url, string id, Channels channel)
    {
        string message = JsonUtility.ToJson(channel);
        var response = await WebRequestHandler.PathchRequest<ChannelResponse>($"{url}/channels/{id}", message);
        return response;
    }
    #endregion

    #region Delete Methodes
    public async Task<bool> DeleteUser(string url, string userId)
    {
        var response = await WebRequestHandler.DeleteRequest($"{url}/users/{userId}");
        return response;
    }
    public async Task<bool> DeleteChannel(string url, string id)
    {
        var response = await WebRequestHandler.DeleteRequest($"{url}/channels/{id}");
        return response;
    }
    #endregion
}
