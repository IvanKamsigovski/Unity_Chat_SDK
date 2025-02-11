using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _url = "http://localhost:3001/api/v1";
    [SerializeField] private int _id = 1;

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

}
