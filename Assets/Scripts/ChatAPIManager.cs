using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<User> GetChanelUser(string url, string id)
    {
        User users = await WebRequestHandler.GetRequest<User>($"{url}/channels/{id}/users");
        return users;
    }
    #endregion

    #region Post Methodes
    public async void CreateUser(string url, User user)
    {
        string message = JsonUtility.ToJson(user);
        await WebRequestHandler.PostRequest($"{url}/users", message);
    }

    public async void CreateChannel(string url, Channels channel)
    {
        string message = JsonUtility.ToJson(channel);
        await WebRequestHandler.PostRequest($"{url}/channels", message);
    }

    public async void AddUserToChannel(string url, string userId, User user)
    {
        string message = JsonUtility.ToJson(user);
        await WebRequestHandler.PostRequest($"{url}/channels/{userId}/add-user", message);
    }

    //Remove user?
    #endregion
}
