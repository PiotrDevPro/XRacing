using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using AuthenticationValues = Photon.Chat.AuthenticationValues;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class UChatRoom : MonoBehaviour, IChatClientListener
{
	public static UChatRoom manage;
	public string[] ChannelsToJoinOnConnect;
	public int HistoryLengthToFetch;
	public InputField InputFieldChat;
	public Text CurrentChannelText;
	public bool isActive = false;
	public string UserName;

	private string selectedChannelName;

	public ChatClient chatClient;

#if !PHOTON_UNITY_NETWORKING
    [SerializeField]
#endif
	protected internal ChatAppSettings chatAppSettings;

	private void Awake()
	{
		manage = this;
	}



	private void Start()
	{

		this.UserName = PlayerPrefs.GetString("Player");
		chatClient = new ChatClient(this);
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(UserName));

		if (string.IsNullOrEmpty(this.UserName))
		{
			this.UserName = "user" + PhotonNetwork.NickName; //made-up username
		}

		bool appIdPresent = !string.IsNullOrEmpty(this.chatAppSettings.AppId);
		if (!appIdPresent)
		{
			Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");
		}
	}

	public void Connect()
	{

		print("chat");
	//	this.chatClient = new ChatClient(this);
#if !UNITY_WEBGL
		this.chatClient.UseBackgroundWorkerForSending = true;
#endif
	//	this.chatClient.AuthValues = new AuthenticationValues(this.UserName);
	//	this.chatClient.ConnectUsingSettings(this.chatAppSettings);

		//this.ChannelToggleToInstantiate.gameObject.SetActive(false);
		//Debug.Log("Connecting as: " + this.UserName);

		//this.ConnectingLabel.SetActive(true);
	}

	public void Disconnect()
	{
		chatClient.Disconnect();

	}

	public void OnDestroy()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	/// <summary>To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnApplicationQuit.</summary>
	public void OnApplicationQuit()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	public void Update()
	{

		if (this.chatClient != null)
		{
			this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
		}
	}

	public void OnEnterSend()
	{
		if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
		{
			this.SendChatMessage(this.InputFieldChat.text);
			this.InputFieldChat.text = "";
		}
	}

	public void OnClickSend()
	{
		if (this.InputFieldChat != null)
		{
			this.SendChatMessage(this.InputFieldChat.text);
			this.InputFieldChat.text = "";
		}
	}

	public int TestLength = 2048;
	private byte[] testBytes = new byte[2048];

	private void SendChatMessage(string inputLine)
	{
		if (string.IsNullOrEmpty(inputLine))
		{
			return;
		}
		if ("test".Equals(inputLine))
		{
			if (this.TestLength != this.testBytes.Length)
			{
				this.testBytes = new byte[this.TestLength];
			}

			this.chatClient.SendPrivateMessage(this.chatClient.AuthValues.UserId, this.testBytes, true);
		}


		bool doingPrivateChat = this.chatClient.PrivateChannels.ContainsKey(this.selectedChannelName);
		string privateChatTarget = string.Empty;
		if (doingPrivateChat)
		{
			// the channel name for a private conversation is (on the client!!) always composed of both user's IDs: "this:remote"
			// so the remote ID is simple to figure out

			string[] splitNames = this.selectedChannelName.Split(new char[] { ':' });
			privateChatTarget = splitNames[1];
		}

		if (inputLine[0].Equals('\\'))
		{
			string[] tokens = inputLine.Split(new char[] { ' ' }, 2);
			//if (tokens[0].Equals("\\help"))
			//{
			//	this.PostHelpToCurrentChannel();
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
				//if (this.channelToggles.ContainsKey(subtokens[0]))
				//{
				//	this.ShowChannel(subtokens[0]);
			}
			//else
			//{
			//	this.chatClient.Subscribe(new string[] { subtokens[0] });
			//}

			Debug.Log("The command '" + tokens[0] + "' is invalid.");

		}
		else
		{
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




	public void DebugReturn(DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
		}
		else
		{
			Debug.Log(message);
		}
	}

	public void OnChatStateChange(ChatState state)
	{
		print(chatClient.State);
		print(UserName);
	}

	public void OnConnected()
	{
		if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
		{
			this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
		}
		print("OnConnected");
	}

	public void OnDisconnected()
	{
		print("OnDisconnected");
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		if (channelName.Equals(this.selectedChannelName))
		{
			// update text
			this.ShowChannel(this.selectedChannelName);
		}
		print("OnGetMessages");
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		print("OnPrivateMessage");
		// as the ChatClient is buffering the messages for you, this GUI doesn't need to do anything here
		// you also get messages that you sent yourself. in that case, the channelName is determinded by the target of your msg
		//this.InstantiateChannelButton(channelName);

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

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		// in this demo, we simply send a message into each channel. This is NOT a must have!
		foreach (string channel in channels)
		{
			this.chatClient.PublishMessage(channel, "entered the chat."); // you don't HAVE to send a msg on join but you could.

			//if (this.ChannelToggleToInstantiate != null)
			//	{
			//	this.InstantiateChannelButton(channel);

			//	}
		}

		Debug.Log("OnSubscribed: " + string.Join(", ", channels));

		// Switch to the first newly created channel
		this.ShowChannel(channels[0]);
	}

	public void ShowChannel(string channelName)
	{

		if (string.IsNullOrEmpty(channelName))
		{
			return;
		}
		ChatChannel channel = null;
		bool found = this.chatClient.TryGetChannel(channelName, out channel);
		if (!found)
		{
			Debug.Log("ShowChannel failed to find channel: " + channelName);
			return;
		}

		this.selectedChannelName = channelName;
		this.CurrentChannelText.text = channel.ToStringMessages();
		//this._lobbyManager.Log(channel.ToStringMessages());
		Debug.Log("ShowChannel: " + this.selectedChannelName);

		//foreach (KeyValuePair<string, Toggle> pair in this.channelToggles)
		//{
		//	pair.Value.isOn = pair.Key == channelName ? true : false;
		//}
	}

	public void AddMessageToSelectedChannel(string msg)
	{
		ChatChannel channel = null;
		bool found = this.chatClient.TryGetChannel(this.selectedChannelName, out channel);
		if (!found)
		{
			Debug.Log("AddMessageToSelectedChannel failed to find channel: " + this.selectedChannelName);
			return;
		}

		if (channel != null)
		{
			channel.Add("Bot", msg, 0); //TODO: how to use msgID?
		}

		print("AddMessageToSelectedChannel");
	}

	public void OnSubscribed(string channel, string[] users, Dictionary<object, object> properties)
	{
		Debug.LogFormat("OnSubscribed: {0}, users.Count: {1} Channel-props: {2}.", channel, users.Length, properties.ToStringFull());
	}

	public void OnUnsubscribed(string[] channels)
	{
		print("OnUnsubscribed");
	}

	public void OnChannelPropertiesChanged(string channel, string userId, Dictionary<object, object> properties)
	{
		Debug.LogFormat("OnChannelPropertiesChanged: {0} by {1}. Props: {2}.", channel, userId, Extensions.ToStringFull(properties));
	}

	public void OnUserPropertiesChanged(string channel, string targetUserId, string senderUserId, Dictionary<object, object> properties)
	{
		Debug.LogFormat("OnUserPropertiesChanged: (channel:{0} user:{1}) by {2}. Props: {3}.", channel, targetUserId, senderUserId, Extensions.ToStringFull(properties));
	}

	/// <inheritdoc />
	public void OnErrorInfo(string channel, string error, object data)
	{
		Debug.LogFormat("OnErrorInfo for channel {0}. Error: {1} Data: {2}", channel, error, data);
	}

	public void OnUserSubscribed(string channel, string user)
	{
		Debug.LogFormat("OnUserSubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
	}

	public void OnUserUnsubscribed(string channel, string user)
	{
		Debug.LogFormat("OnUserUnsubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
	}
}

