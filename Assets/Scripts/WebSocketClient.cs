using System;
using NativeWebSocket;
using UnityEngine;

public static class WebSocketClient 
{
    public static Action<byte[]>  OnMessage;

    private static WebSocket _websocket;

    /// <summary>
    /// Starts the socket with correct url and hendles the events
    /// </summary>
    /// <param name="socketUrl"></param>
    public static async void StartSocket(string socketUrl)
    {
        _websocket = new WebSocket(socketUrl);

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
            OnMessage?.Invoke(bytes);
        };

        // waiting for messages
        await _websocket.Connect();
    }
    /// <summary>
    /// Constatly dispatches message queue, This should be called in Update
    /// </summary>
    public static void DispatchMessage() => _websocket.DispatchMessageQueue();

    /// <summary>
    /// Handle disconnection, amke sure to call this when you want to stop the socket
    /// </summary>
    public static async void StopSocket()
    {
        await _websocket.Close();
    }

    public static async void SendMessage(byte[] message)
    {
        if (_websocket.State == WebSocketState.Open)
            await _websocket.Send(message);
    }

    public static async void SendMessage(string message)
    {
        if (_websocket.State == WebSocketState.Open)
            await _websocket.SendText(message);
    }
}
