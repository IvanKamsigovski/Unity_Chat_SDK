using System;
using UnityEngine;
using ChatSDK.ChatClient;

public class MessageHandler : MonoBehaviour
{
    public static MessageHandler Instance { get; private set; }
    /// <summary>
    /// Use this event to proccese incoming message
    /// It uses Messages object, but can be modified to use byte array
    /// </summary>
    public Action<Messages> OnMessageProccess;
    [SerializeField] private string _socketUrl = "ws://localhost:3001/api/v1/messages";
    [SerializeField] private bool _starOnAvake = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        WebSocketClient.OnMessage += OnMessageReceived;

        if (_starOnAvake)
            WebSocketClient.StartSocket(_socketUrl);
    }

    private void OnDisable()
    {
        WebSocketClient.OnMessage -= OnMessageReceived;
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        WebSocketClient.DispatchMessage();
#endif
    }

    private void OnApplicationQuit()
    {
        WebSocketClient.StopSocket();
    }
    /// <summary>
    /// Used when we donn't want socket to start on awake
    /// </summary>
    public void StartSocketManually() => WebSocketClient.StartSocket(_socketUrl);

    public void OnMessageReceived(byte[] bytes)
    {
        try
        {
            string jsonMessage = System.Text.Encoding.UTF8.GetString(bytes);
            Messages messageData = JsonUtility.FromJson<Messages>(jsonMessage);

            Debug.Log("Message received: " + jsonMessage);
            OnMessageProccess?.Invoke(messageData);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error On Message Recived: " + e.Message);
        }
    }


    /// <summary>
    /// Called to send messages with webosocket
    /// </summary>
    /// <param name="message">Message byte array</param>
    public void OnMessageSent(byte[] message)
    {
        WebSocketClient.SendMessage(message);
    }
    /// <summary>
    /// Called to send messages with webosocket
    /// </summary>
    /// <param name="message">Message string</param>
    public void OnMessageSent(string message)
    {
        WebSocketClient.SendMessage(message);
    }
}
