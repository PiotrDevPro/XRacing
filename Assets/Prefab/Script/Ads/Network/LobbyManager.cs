using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager manage;
    public Text LogText;
    public Button createRoomHighway;
    public Button createRoomCity;
    public ChatClient chatClient;
    public GameObject connectToServerMsg;
    public GameObject loading;
    int count = 0;
    private GameObject _player;
    private void Awake()
    {
        manage = this;
        /*
        if (manage != null && manage != this)
            gameObject.SetActive(false);
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        */
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        loading.SetActive(false);
        connectToServerMsg.SetActive(true);
    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {

            PhotonNetwork.NickName = PlayerPrefs.GetString("Player");
            //Log("Player's name is set to " + PhotonNetwork.NickName);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
            _player.GetComponent<PhotonView>().enabled = true;
        }
    }


    public override void OnConnectedToMaster()
    {
        Amplitude.Instance.logEvent("ConnectedToMasterServer");
        createRoomHighway.interactable = true;
        createRoomCity.interactable = true;
        connectToServerMsg.SetActive(false);

    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Highway", new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
        Amplitude.Instance.logEvent("CreateRoomHighway");

    }

    public void CreateRoomCity()
    {
        PhotonNetwork.CreateRoom("City", new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
        Amplitude.Instance.logEvent("CreateRoomCity");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Highway");
        Amplitude.Instance.logEvent("JoinRoomHighway");
        //PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoomCity()
    {
        PhotonNetwork.JoinRoom("City");
        Amplitude.Instance.logEvent("JoinRoomCity");
    }

    public override void OnJoinedRoom()
    {
        
        if (PhotonNetwork.CurrentRoom.Name == "Highway")
        {
            loading.SetActive(true);
            createRoomHighway.interactable = false;
            print(PhotonNetwork.CurrentRoom);
            PhotonNetwork.LoadLevel("battle_online");
            MainMenuManager.manage.LoadEngineUpgradeOnSelectedCar();
            MainMenuManager.manage.LoadHandlingOnSelectedCar();
            MainMenuManager.manage.LoadBrakeOnSelectedCar();
        }
        else if (PhotonNetwork.CurrentRoom.Name == "City")
        {
            createRoomCity.interactable = false;
            print(PhotonNetwork.CurrentRoom);
            PhotonNetwork.LoadLevel("city_online");
            MainMenuManager.manage.LoadEngineUpgradeOnSelectedCar();
            MainMenuManager.manage.LoadHandlingOnSelectedCar();
            MainMenuManager.manage.LoadBrakeOnSelectedCar();
        }

        Time.timeScale = 1;
    }

    public void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;

    }

    public void DeactiveNetwork()
    {
        PhotonNetwork.Disconnect();
        LogText.text = "LOG:" + "";
        count = 0;
        Amplitude.Instance.logEvent("QuitNetworkRoom");
        connectToServerMsg.SetActive(true);
        createRoomHighway.interactable = false;
        _player.GetComponent<PhotonView>().enabled = false;
        print(PhotonNetwork.NetworkClientState);
    }
}

