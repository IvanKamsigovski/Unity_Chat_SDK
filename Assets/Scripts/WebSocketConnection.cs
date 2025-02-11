using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NativeWebSocket;
using UnityEngine;

public class WebSocketConnection : MonoBehaviour
{
    private WebSocket _websocket;
    [SerializeField] private string _socketUrl = "ws://localhost:3001/api/v1/messages";

    private void Start()
    {
        StartSocket();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _websocket.DispatchMessageQueue();
#endif
    }

    public async void StartSocket()
    {
        _websocket = new WebSocket(_socketUrl);

        _websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        _websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        _websocket.OnClose += (e) =>
        {
            Debug.Log("On Message");
            Debug.Log("Connection closed!");
        };

        _websocket.OnMessage += (bytes) =>
        {
            // OnMessageReceived(System.Text.Encoding.UTF8.GetString(bytes));
            OnMessageReceived(bytes);
        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await _websocket.Connect();
    }


    public async void StopSocket()
    {
        await _websocket.Close();
    }


    public void OnMessageReceived(string message)
    {
        Debug.Log("Message received: " + message);
    }

    private void OnMessageReceived(byte[] bytes)
    {
        try
        {
            string jsonMessage = System.Text.Encoding.UTF8.GetString(bytes);
            Messages messageData = JsonUtility.FromJson<Messages>(jsonMessage);

            Debug.Log("Message received: " + messageData);
            ProcessMessage(messageData);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error On Message Recived: " + e.Message);
        }

    }

    public Messages ProcessMessage(Messages message) => message;

    async void SendWebSocketMessage()
    {
        if (_websocket.State == WebSocketState.Open)
        {
            Debug.Log("Sending message...");
            // Sending bytes
            await _websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await _websocket.SendText("plain text message");
        }
    }

    public async void SendMessage(Messages message)
    {
        string messageJson = JsonUtility.ToJson(message);
        if (_websocket.State == WebSocketState.Open)
            await _websocket.SendText(messageJson);
    }

    private void OnApplicationQuit()
    {
        StopSocket();
    }
}
