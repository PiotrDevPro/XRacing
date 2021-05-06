using Photon.Pun;
using Photon.Realtime;
using Photon;
using Photon.Chat;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System.Collections.Generic;

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

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;
    public Player Player { get; private set; }
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
            //PlayerPrefs.SetString("PlayerNetwork",PhotonNetwork.NickName);
            //print(PlayerPrefs.GetString("PlayerNetwork"));
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
        PhotonNetwork.JoinLobby();
        createRoomHighway.interactable = true;
        createRoomCity.interactable = true;
        connectToServerMsg.SetActive(false);

    }

    public void CreateRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.MaxPlayers = 10;
        ro.IsVisible = true;
        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("Energy", 1);
        ro.CustomRoomProperties = RoomCustomProps;
        PhotonNetwork.JoinOrCreateRoom("Highway", ro, TypedLobby.Default) ;
        Amplitude.Instance.logEvent("CreateRoomHighway");
        loading.SetActive(true);
    }

    public void CreateRoomCity()
    {
        PhotonNetwork.CreateRoom("City", new RoomOptions { MaxPlayers = 10 });
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

    public void JoinRoomOnClicked(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnJoinedRoom()
    {
        
        if (PhotonNetwork.CurrentRoom.Name == "Highway")
        {
            
            createRoomHighway.interactable = false;
           
            MainMenuManager.manage.LoadEngineUpgradeOnSelectedCar();
            MainMenuManager.manage.LoadHandlingOnSelectedCar();
            MainMenuManager.manage.LoadBrakeOnSelectedCar();
            Hashtable playerCustomProps = new Hashtable();
            playerCustomProps.Add("Role", 2);
            PhotonNetwork.SetPlayerCustomProperties(playerCustomProps);
            print("Highway");
            PhotonNetwork.LoadLevel("battle_online");

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

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("OnRoomListUpdate");
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListPrefab, roomListContent).GetComponent<roomListItem>().SetUp(roomList[i]);

        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");

    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
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

