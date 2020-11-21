using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class LobbyManager :  MonoBehaviourPunCallbacks 
{
    public static LobbyManager manage;
    public Text LogText;
    public Button createRoomConnect;
    public ChatClient chatClient;
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
        print("Connected to master server");
        createRoomConnect.interactable = true;

    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 8 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        
    }

    public override void OnJoinedRoom()
    {
        print("Joined the room");
        createRoomConnect.interactable = false;
        PhotonNetwork.LoadLevel("battle_online");
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
        LogText.text ="LOG:" + "";
        count = 0;
        Amplitude.Instance.logEvent("QuitNetworkRoom");
        createRoomConnect.interactable = false;
        _player.GetComponent<PhotonView>().enabled = false;
        print(PhotonNetwork.NetworkClientState);
    }

        //public void Rename()
        // {
        //     PhotonNetwork.NickName = ;
        //LogText.text = "LOG:" + "";
        //   Log("Player rename to " + PhotonNetwork.NickName);
        //   PhotonNetwork.AutomaticallySyncScene = true;
        //  PhotonNetwork.GameVersion = "1";
        //   PhotonNetwork.ConnectUsingSettings();
        //}

    /*


    #region Chat Text

    public int TestLength = 2048;
    private byte[] testBytes = new byte[2048];

    public void OnClickSend()
    {
        if (this.InputFieldChat != null)
        {
            this.SendChatMessage(this.InputFieldChat.text);
            this.InputFieldChat.text = "";
            print("OnClickSend");
        }
    }

    private void SendChatMessage(string inputLine)
    {
       //if (string.IsNullOrEmpty(inputLine))
        //{
       //     return;
        //}
        if ("test".Equals(inputLine))
        {
            
           // if (this.TestLength != this.testBytes.Length)
          //  {
           //     this.testBytes = new byte[this.TestLength];
          //      print("SendPrivateMessageTestBytes");
          //  }

            this.chatClient.SendPrivateMessage(this.chatClient.AuthValues.UserId, this.testBytes, true);
            
        }


        bool doingPrivateChat = this.chatClient.PrivateChannels.ContainsKey(this.selectedChannelName);
        string privateChatTarget = string.Empty;
        print("SendChatMessage");
        if (doingPrivateChat)
        {
            // the channel name for a private conversation is (on the client!!) always composed of both user's IDs: "this:remote"
            // so the remote ID is simple to figure out

            string[] splitNames = this.selectedChannelName.Split(new char[] { ':' });
            privateChatTarget = splitNames[1];
        }
        //UnityEngine.Debug.Log("selectedChannelName: " + selectedChannelName + " doingPrivateChat: " + doingPrivateChat + " privateChatTarget: " + privateChatTarget);


        if (inputLine[0].Equals('\\'))
        {
            string[] tokens = inputLine.Split(new char[] { ' ' }, 2);
            //if (tokens[0].Equals("\\help"))
            //{
            //    this.PostHelpToCurrentChannel();
            //}
            if (tokens[0].Equals("\\state"))
            {
                int newState = 0;


                List<string> messages = new List<string>();
                messages.Add("i am state " + newState);
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' });

                if (subtokens.Length > 0)
                {
                    newState = int.Parse(subtokens[0]);
                }

                if (subtokens.Length > 1)
                {
                    messages.Add(subtokens[1]);
                }

                this.chatClient.SetOnlineStatus(newState, messages.ToArray()); // this is how you set your own state and (any) message
            }
            else if ((tokens[0].Equals("\\subscribe") || tokens[0].Equals("\\s")) && !string.IsNullOrEmpty(tokens[1]))
            {
                this.chatClient.Subscribe(tokens[1].Split(new char[] { ' ', ',' }));
            }
            else if ((tokens[0].Equals("\\unsubscribe") || tokens[0].Equals("\\u")) && !string.IsNullOrEmpty(tokens[1]))
            {
                this.chatClient.Unsubscribe(tokens[1].Split(new char[] { ' ', ',' }));
            }
            else if (tokens[0].Equals("\\clear"))
            {
                if (doingPrivateChat)
                {
                    this.chatClient.PrivateChannels.Remove(this.selectedChannelName);
                }
                else
                {
                    ChatChannel channel;
                    if (this.chatClient.TryGetChannel(this.selectedChannelName, doingPrivateChat, out channel))
                    {
                        channel.ClearMessages();
                    }
                }
            }
            else if (tokens[0].Equals("\\msg") && !string.IsNullOrEmpty(tokens[1]))
            {
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);
                if (subtokens.Length < 2) return;

                string targetUser = subtokens[0];
                string message = subtokens[1];
                this.chatClient.SendPrivateMessage(targetUser, message);
            }
            else if ((tokens[0].Equals("\\join") || tokens[0].Equals("\\j")) && !string.IsNullOrEmpty(tokens[1]))
            {
                string[] subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);

                // If we are already subscribed to the channel we directly switch to it, otherwise we subscribe to it first and then switch to it implicitly
                // if (this.channelToggles.ContainsKey(subtokens[0]))
                //{
               ///      this.ShowChannel(subtokens[0]);
                //  }
                // else
                //   {
                //      this.chatClient.Subscribe(new string[] { subtokens[0] });
                //   }
                //   }
                //  else
                //  {
                //      Debug.Log("The command '" + tokens[0] + "' is invalid.");
                //  }
                //   }
                //  else
                //  {
                if (doingPrivateChat)
                {
                    this.chatClient.SendPrivateMessage(privateChatTarget, inputLine);
                }
                else
                {
                    this.chatClient.PublishMessage(this.selectedChannelName, inputLine);
                }
            }
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnDisconnected()
    {
        print("OnDisconnected");
    }

    public void OnChatStateChange(ChatState state)
    {
        print(chatClient.State);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        print("OnGetMessages");
        if (channelName.Equals(this.selectedChannelName))
        {
            print("OnGetMessageSelectedChannelName");
             update text
            this.ShowChannel(this.selectedChannelName);
        }
    }

    public void ShowChannel(string channelName)
    {
        print("ShowChannel");
        if (string.IsNullOrEmpty(channelName))
        {
            print("ShowChannelIsEmpty");
            return;
        }

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }

        //this.selectedChannelName = channelName;
        //this.ChannelText.text = channel.ToStringMessages();
        //Debug.Log("ShowChannel: " + this.selectedChannelName);

       // foreach (KeyValuePair<string, Toggle> pair in this.channelToggles)
       // {
         //   pair.Value.isOn = pair.Key == channelName ? true : false;
      //  }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        byte[] msgBytes = message as byte[];
        if (msgBytes != null)
        {
            Debug.Log("Message with byte[].Length: " + msgBytes.Length);
        }
        if (this.selectedChannelName.Equals(channelName))
        {
            this.ShowChannel(channelName);
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        // in this demo, we simply send a message into each channel. This is NOT a must have!
        foreach (string channel in channels)
        {
            this.chatClient.PublishMessage(channel, "says 'hi'."); // you don't HAVE to send a msg on join but you could.

           // if (this.ChannelToggleToInstantiate != null)
           // {
            //    this.InstantiateChannelButton(channel);

           // }
        }

        Debug.Log("OnSubscribed: " + string.Join(", ", channels));

        /*
        // select first subscribed channel in alphabetical order
        if (this.chatClient.PublicChannels.Count > 0)
        {
            var l = new List<string>(this.chatClient.PublicChannels.Keys);
            l.Sort();
            string selected = l[0];
            if (this.channelToggles.ContainsKey(selected))
            {
                ShowChannel(selected);
                foreach (var c in this.channelToggles)
                {
                    c.Value.isOn = false;
                }
                this.channelToggles[selected].isOn = true;
                AddMessageToSelectedChannel(WelcomeText);
            }
        }
        

        // Switch to the first newly created channel
        this.ShowChannel(channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.LogFormat("OnUserSubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.LogFormat("OnUserUnsubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
    }

    /// <inheritdoc />
    public void OnChannelPropertiesChanged(string channel, string userId, Dictionary<object, object> properties)
    {
        Debug.LogFormat("OnChannelPropertiesChanged: {0} by {1}. Props: {2}.", channel, userId);
    }

    public void OnUserPropertiesChanged(string channel, string targetUserId, string senderUserId, Dictionary<object, object> properties)
    {
        Debug.LogFormat("OnUserPropertiesChanged: (channel:{0} user:{1}) by {2}. Props: {3}.", channel, targetUserId, senderUserId);
    }


    public void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }


    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    #endregion

    */
}
