using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ChatSDK;

public class TestUIController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _url = "http://localhost:3001/api/v1";
    [SerializeField] private int _id = 1;
    [SerializeField] private int _deleteID = 2;

    [Header("UI")]
    [SerializeField] private Button _getUsersBtn;
    [SerializeField] private Button _getUserOfIDBtn;
    [SerializeField] private Button _getChannels;
    [SerializeField] private Button _getChannelOfIDBtn;
    [SerializeField] private Button _createUser;
    [SerializeField] private Button _addUserToChannel;
    [SerializeField] private Button _updateUser;
    [SerializeField] private Button _deleteUser;

    [SerializeField] private TMP_Text _responseString;



    public void Start()
    {
        _getUsersBtn.onClick.AddListener(GetUsers);
        _getUserOfIDBtn.onClick.AddListener(GetUser);
        _getChannels.onClick.AddListener(GetChannels);
        _getChannelOfIDBtn.onClick.AddListener(GetChannel);
        _createUser.onClick.AddListener(CreateUser);
        _addUserToChannel.onClick.AddListener(AddUserToChannel);
        _updateUser.onClick.AddListener(UpdateUser);
        _deleteUser.onClick.AddListener(DeleteUser);
    }

    private void HandleResponse(string response)
    {
       _responseString.text = response;
    }

    public async void GetUsers()
    {

        await ChatAPIManager.GetUsers(_url, HandleResponse);
    }


    public async void GetUser()
    {
        await ChatAPIManager.GetUser(_url, _id, HandleResponse);
    }

    public async void GetChannels()
    {
        await ChatAPIManager.GetChannels(_url, HandleResponse);
    }

    public async void GetChannel()
    {
        await ChatAPIManager.GetChannel(_url, _id, HandleResponse);
    }

    public async void CreateUser()
    {
       await ChatAPIManager.CreateUser(_url, new User(),HandleResponse);
    }

    public async void AddUserToChannel()
    {
       await ChatAPIManager.AddUserToChannel(_url, 5, new User(),HandleResponse);
    }

    public async void UpdateUser()
    {
       await ChatAPIManager.UpdateUser(_url, _deleteID, new User(),HandleResponse);
    }

    public async void DeleteUser()
    {
       await ChatAPIManager.DeleteUser(_url, _id,HandleResponse);
    }

}
