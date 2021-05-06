using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon;

public class roomListItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _textMaxPlayer;
    [SerializeField]
    private Text _textCurrPlayer;
    [SerializeField]
    private Text _textRoomName;
    [SerializeField]
    private GameObject _loading;


    public RoomInfo RoomInfo;

    public void SetUp(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        //_playersCountProgress.minValue = 0;
        //_playersCountProgress.maxValue = roomInfo.MaxPlayers;
        _textCurrPlayer.text = roomInfo.PlayerCount.ToString();
        _textMaxPlayer.text = roomInfo.MaxPlayers.ToString();
        _textRoomName.text = roomInfo.Name;
    }
    public void OnClick_Button()
    {
        LobbyManager.manage.JoinRoomOnClicked(RoomInfo);
        _loading.SetActive(true);
    }
}
