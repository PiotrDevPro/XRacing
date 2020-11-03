using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class netManager : MonoBehaviourPunCallbacks
{
    public static netManager manage;
    public GameObject[] CarsPrefabs;
    private GameObject InstantiatedCar;
    public Transform spawnPoint;
    public Transform PlayerCarPoint;
    public Text maxspd;
    public GameObject NetworkMsg;
    int count = 0;
    private float starttime = 5f;
    private float curr = 0;

    private void Awake()
    {
       manage = this;
    }

    private void Start()
    {
        curr = starttime;
        InstantiatedCar = PhotonNetwork.Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")].name,spawnPoint.position, spawnPoint.rotation);
        Amplitude.Instance.logEvent("LevelNetworkStart");
        
        //InstantiatedCar.GetComponent<BoxCollider>().tag = "";

    }

    private void Update()
    {
        maxspd.text = "MAX:" + InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed.ToString() + "KM/H";
        if (InstantiatedCar.GetComponent<RCC_CarControllerV3>().speed > 30f)
        {
            count += 1;
            if (count == 1)
            {
                Amplitude.Instance.logEvent("StartRide > 30 kmh");

            }
        }
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        Time.timeScale = 1;

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("garage");
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room", otherPlayer.NickName);
    }

    void Timer()
    {
        print(curr);
        curr -= 1 * Time.deltaTime;
        if (curr <= 0)
        {
            curr = 0;
        }
    }

}
