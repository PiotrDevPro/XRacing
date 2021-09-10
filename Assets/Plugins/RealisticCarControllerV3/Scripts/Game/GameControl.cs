using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl manage;

    [Header("Main")]
    public GameObject Pause_online;
    public GameObject Pause_single;
    public GameObject CarSpawn_single;
    public GameObject CarSpawn_online;
    public GameObject rcc_photon_manager;
    //public GameObject rcc_scene_manager;
    public GameObject chatActive;
    public GameObject chatRoomPanel;
    private void Awake()
    {
        if (manage != null)
        {
            manage = this;
        }

        if (MainMenuManager.manage.isCity)
        {
            
        }
    }

    private void Start()
    {
            CarSpawn_single.SetActive(true);
            CarSpawn_online.SetActive(false);
            rcc_photon_manager.SetActive(false);
            Pause_single.SetActive(false);
            Pause_online.SetActive(false);
            chatActive.SetActive(false);
            chatRoomPanel.SetActive(false);

    }
}
