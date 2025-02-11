using System;
using NativeWebSocket;
using UnityEngine;

namespace ChatSDK.ChatClient
{
    public static class WebSocketClient
    {
        public static Action<byte[]> OnMessage;

        private static WebSocket _websocket;

        public static bool IsSocketStaerted = false;

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
            
            await _websocket.Connect();

            IsSocketStaerted  = true;
        }
        /// <summary>
        /// Constatly dispatches message queue, This should be called in Update
        /// </summary>
        public static void DispatchMessage()
        {
            if(IsSocketStaerted)
                 _websocket.DispatchMessageQueue();
        }

        /// <summary>
        /// Handle disconnection, amke sure to call this when you want to stop the socket
        /// </summary>
        public static async void StopSocket()
        {
            if(!IsSocketStaerted) return;

            await _websocket.Close();
            IsSocketStaerted = false;
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

}
