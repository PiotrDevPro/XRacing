using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseNotifController : MonoBehaviour
{
    private void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenRecived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Recived New Msg:" + e.Message.From);
    }

    private void OnTokenRecived(object sender, TokenReceivedEventArgs e)
    {
        Debug.Log("Recived Registration Token:" + e.Token);
    }
}
