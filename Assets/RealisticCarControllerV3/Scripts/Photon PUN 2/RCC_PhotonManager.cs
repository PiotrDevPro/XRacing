using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;
using Photon.Realtime;

/// <summary>
/// Connects to Photon Server, registers the player, and activates player UI panel when connected.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Network/Photon/RCC Photon Scene Manager")]
public class RCC_PhotonManager : MonoBehaviourPunCallbacks {

	public Player player;
	void Start () {

		ConnectToServer();
	}

	void ConnectToServer () {

		print("Connecting to photon server");
		RCC_InfoLabel.Instance.ShowInfo ("Connecting to photon server");

		if (!Photon.Pun.PhotonNetwork.IsConnectedAndReady) {

			Photon.Pun.PhotonNetwork.ConnectUsingSettings ();

		}

		if (Photon.Pun.PhotonNetwork.IsConnectedAndReady) {

		}
	
	}

	public override void OnConnectedToMaster(){

		print ("Connected to master server");
		Photon.Pun.PhotonNetwork.JoinLobby ();

	}

	void OnGUI(){

		if(!Photon.Pun.PhotonNetwork.IsConnectedAndReady)
			GUI.color = Color.red;
		GUILayout.Label("State: " + Photon.Pun.PhotonNetwork.NetworkClientState.ToString());
		GUI.color = Color.white;
		GUILayout.Label("Name: " + PhotonNetwork.NickName);
		GUILayout.Label("Total Players: " + Photon.Pun.PhotonNetwork.PlayerList.Length.ToString());
		GUILayout.Label("Ping: " + Photon.Pun.PhotonNetwork.GetPing().ToString());

	}

	public override void OnJoinedLobby(){

		print("Joined lobby");
		RCC_InfoLabel.Instance.ShowInfo ("Joined Lobby");
		Photon.Pun.PhotonNetwork.JoinRandomRoom();

	}

	public override void OnJoinRandomFailed(short a, string b){

		print("Joining to random room has failed!, Creating new room...");
		RCC_InfoLabel.Instance.ShowInfo ("Joining to random room has failed!, Creating new room...");
		Photon.Pun.PhotonNetwork.CreateRoom(null);

	}

	

	public override void OnJoinedRoom(){

		print("Joined room");

		RCC_InfoLabel.Instance.ShowInfo ("Joined Room. You can spawn your vehicle.");
		

	}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
		
	}

    public void Leave()
	{
		PhotonNetwork.LeaveRoom();
		Time.timeScale = 1;
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("garage");
		PhotonNetwork.Disconnect();
		Amplitude.Instance.logEvent("PlayerLeaveTheRoom");

	}


	public void SetPlayerName(string name){

		Photon.Pun.PhotonNetwork.NickName = name;
		//playerName.gameObject.SetActive(false);
		//RCC_SceneManager.Instance.activePlayerCanvas.SetDisplayType (RCC_UIDashboardDisplay.DisplayType.Full);

	}

}
