using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UserNameConnected : MonoBehaviour
{
    private string UserNamePlayerPref = "Player";

    public UChat chatNewComponent;

    public InputField idInput;

    private void Start()
    {
        this.chatNewComponent = FindObjectOfType<UChat>();
        string prefsName = PlayerPrefs.GetString(UserNamePlayerPref);
        if (!string.IsNullOrEmpty(prefsName))
        {
            this.idInput.text = prefsName;
        }
    }

    public void EndEditOnEnter()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            this.StartChat();
        }
    }

    public void StartChat()
    {
        UChat chatNewComponent = FindObjectOfType<UChat>();
        chatNewComponent.UserName = this.idInput.text.Trim();
        chatNewComponent.Connect();
        //enabled = false;

        if (this.idInput.text == null)
        {
            PlayerPrefs.SetString(UserNamePlayerPref, chatNewComponent.UserName + Random.Range(0,999));
        }
        else
        {
            PlayerPrefs.SetString(UserNamePlayerPref, chatNewComponent.UserName);
        }
            MainMenuManager.manage.levelBattleOnline();
    }
}
