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
    public GameObject loadingToCityMap;
    public GameObject loadingPanel;
    public bool isCityLoadingBtnClicked = false;
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
        loadingPanel.SetActive(false);
        loadingToCityMap.SetActive(false);

    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            loading.SetActive(false);
            loadingToCityMap.SetActive(false);
            loadingPanel.SetActive(false);
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

    public void LoadCityOnlineFromAB() 
    {
        isCityLoadingBtnClicked = true;
        if (PlayerPrefs.GetInt("AppActivate") == 0)
        {
            PlayerPrefs.SetInt("AppActivate",1);
        }

         if (PlayerPrefs.GetInt("AppActivate") ==1)
        {
                LoadAssetsBundle.manage.LoadAssetBundleSceneCity();
                Amplitude.Instance.logEvent("LoadAssetBundleSceneCity");
        }
        else
        {
            CreateRoomCity();
            Amplitude.Instance.logEvent("LoadingCityOnlineMap");
        }
    }
    

    public void CreateRoomCity()
    {
        PhotonNetwork.JoinOrCreateRoom("City", new RoomOptions { MaxPlayers = 10 }, TypedLobby.Default);
        Amplitude.Instance.logEvent("CreateRoomCity");
        loadingToCityMap.SetActive(true);
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
        createRoomCity.interactable = false;
        Amplitude.Instance.logEvent("JoinRoomCity");
    }

    public void JoinRoomOnClicked(RoomInfo info)
    {
        if (info.Name != "City")
        {
            PhotonNetwork.JoinRoom(info.Name);
        }
        if (info.Name == "City")
        {
            LoadCityOnlineFromAB();
        }
    }

    public override void OnJoinedRoom()
    {
        
        if (PhotonNetwork.CurrentRoom.Name == "Highway")
        {
            
            createRoomHighway.interactable = false;
            loadingPanel.SetActive(true);
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
            loadingPanel.SetActive(true);
            print(PhotonNetwork.CurrentRoom);
            PhotonNetwork.LoadLevel("city_online");
            LoadAssetsBundle.manage.loadingPanel.SetActive(false);
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

