using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine.UI;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public static PlayFabLogin manage;

    [SerializeField] private string username;
    [SerializeField] bool LoginWithCustomId_ = false;
    [SerializeField] Button LeaderBoardActive;
    [SerializeField] Button RatingBoardActive;
    [Header("Player Statistic's")]
    [SerializeField] Text name1;
    [SerializeField] Text name2;
    [SerializeField] Text name3;
    [SerializeField] Text carscore;
    [SerializeField] Text coinz_;
    [SerializeField] Text rating_count;
    [SerializeField] Text rank1;
    [SerializeField] Text rank2;
    [SerializeField] Text rank3;

    private void OnEnable()
    {
        if (manage == null)
        {
            manage = this;
        } else
        {
            if (manage != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Awake()
    {

        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "B0DF2";
        }
    }

    private void Update()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            LeaderBoardActive.interactable = true;
            RatingBoardActive.interactable = true;
        }
        else
        {
            LeaderBoardActive.interactable = false;
            RatingBoardActive.interactable = false;
        }
    }

    private void Start()
    {
        print(PlayerPrefs.GetString("Player"));
        SetUserName();
        Login();
    }
    private void Login()
    {
        if (!IsValidUserName()) return;

        LoginWithCustomId();
    }

    private bool IsValidUserName()
    {
        bool isValid = false;

        // if (username.Length >= 3 && username.Length <= 24)
        //     isValid = true;
        if (PlayerPrefs.GetString("Player") != "")
            isValid = true;

        return isValid;
    }


    private void SetUserName()
    {
        username = PlayerPrefs.GetString("Player");
    }

    private void LoginWithCustomId()
    {
        print("Login to Playfab as " + username);
        var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
        LoginWithCustomId_ = true;
    }

    private void UpdateDisplayName(string displayname)
    {
        print("Display Name" + displayname);
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        print("You have update Displayname!");
        SetStats();
    }

    private void OnLoginCustomIdSuccess(LoginResult obj)
    {
        print("You have logged into Playfab using custom id" + PlayerPrefs.GetString("Player"));
        UpdateDisplayName(PlayerPrefs.GetString("Player"));
    }

    private void OnFailure(PlayFabError error)
    {
        print("error" + error.GenerateErrorReport());
    }

    #region Player Statistics

    public int carDestroy;
    public int coinz;
    public int rating;


    public void SetStats()
    {
        carDestroy = PlayerPrefs.GetInt("fragAI");
        coinz = (int)PlayerPrefs.GetFloat("DriftCoin");
        rating = PlayerPrefs.GetInt("Rating");

        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
        {
            new StatisticUpdate { StatisticName = "CarDestroy", Value = carDestroy},
            new StatisticUpdate { StatisticName = "Coinz", Value = coinz},
            new StatisticUpdate { StatisticName = "Rating", Value = rating},
        }


        },

        result => { Debug.Log("User Statistics Updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });

    
    }


    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport()));
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Recieved the folowing Statistics");

        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic {" + eachStat.StatisticName + "}:" + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "CarDestroy":
                    carDestroy = eachStat.Value;
                    break;
                case "Coinz":
                    coinz = eachStat.Value;
                    break;
                case "Rating":
                    rating = eachStat.Value;
                    break;
            }
        }
    }
    #endregion

    #region Lederboard

    public GameObject listingCDPrefab;
    public Transform listingCDContainer;

    public GameObject listingCoinzPrefab;
    public Transform listingCoinzContainer;

    public GameObject listingRatingPrefab;
    public Transform listingRatingContainer;
    public void GetLeaderboard()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "CarDestroy", MaxResultsCount = 50 };

        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboardCarDestroy, OnErrorLeaderboard);
    }

    public void GetLeaderboardCoinz()
    {
        var requestLeaderboardCoinz = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Coinz", MaxResultsCount = 50 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboardCoinz, OnGetLeaderboardCoinz, OnErrorLeaderboard);
    }

    public void GetRatingboard()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Rating", MaxResultsCount = 50 };

        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetRatingboard, OnErrorLeaderboard);
    }


    void OnGetLeaderboardCarDestroy(GetLeaderboardResult result)
    {
        //Debug.Log(result.Leaderboard[0].StatValue);
        name1.text = username;
        carscore.text = ((int)PlayerPrefs.GetInt("fragAI")).ToString();
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingCDPrefab,listingCDContainer);
            LeaderBoardList LL = tempListing.GetComponent<LeaderBoardList>();
            LL.playerName.text = player.DisplayName;
            LL.playerScore.text = player.StatValue.ToString();
            LL.pos.text = "#" + (player.Position+1).ToString();
            if (LL.pos.text == "#1")
            {
                LL.pos.color = Color.green;
                LL.playerName.color = Color.green;
                LL.crown_ico.SetActive(true);
            }
                
            if (LL.pos.text == "#2")
            {
                LL.pos.color = Color.yellow;
                LL.playerName.color = Color.yellow;
                LL.crown_ico.SetActive(false);
            }
                
            if (LL.pos.text == "#3")
            {
                LL.pos.color = Color.red;
                LL.playerName.color = Color.red;
                LL.crown_ico.SetActive(false);
            }
        }
    }

    void OnGetRatingboard(GetLeaderboardResult result)
    {
        //Debug.Log(result.Leaderboard[0].StatValue);
        name3.text = username;
        rating_count.text = PlayerPrefs.GetInt("Rating").ToString();
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingRatingPrefab, listingRatingContainer);
            LeaderBoardList LL = tempListing.GetComponent<LeaderBoardList>();
            LL.playerName.text = player.DisplayName;
            LL.playerScore.text = player.StatValue.ToString();
            LL.pos.text = "#" + (player.Position + 1).ToString();
            if (LL.pos.text == "#1")
            {
                LL.pos.color = Color.green;
                LL.playerName.color = Color.green;
                LL.crown_ico.SetActive(true);
            }

            if (LL.pos.text == "#2")
            {
                LL.pos.color = Color.yellow;
                LL.playerName.color = Color.yellow;
                LL.crown_ico.SetActive(false);
            }

            if (LL.pos.text == "#3")
            {
                LL.pos.color = Color.red;
                LL.playerName.color = Color.red;
                LL.crown_ico.SetActive(false);
            }
        }
    }

    void OnGetLeaderboardCoinz(GetLeaderboardResult result)
    {
        //Debug.Log(result.Leaderboard[0].StatValue);
        name2.text = username;
        coinz_.text = ((int)PlayerPrefs.GetFloat("DriftCoin")).ToString();
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingCoinzPrefab, listingCoinzContainer);
            LeaderBoardList LL = tempListing.GetComponent<LeaderBoardList>();
            LL.playerName.text = player.DisplayName;
            LL.playerScore.text = player.StatValue.ToString();
            LL.pos.text = "#" + (player.Position + 1).ToString();
            if (LL.pos.text == "#1")
            {
                LL.pos.color = Color.green;
                LL.playerName.color = Color.green;
                LL.crown_ico.SetActive(true);
            }

            if (LL.pos.text == "#2")
            {
                LL.pos.color = Color.yellow;
                LL.playerName.color = Color.yellow;
                LL.crown_ico.SetActive(false);
            }

            if (LL.pos.text == "#3")
            {
                LL.pos.color = Color.red;
                LL.playerName.color = Color.red;
                LL.crown_ico.SetActive(false);
            }
        }
    }

    public void CloseLeaderboardPanel()
    {
        for (int i=listingCDContainer.childCount - 1; i >=0; i--)
        {
            Destroy(listingCDContainer.GetChild(i).gameObject);
        }
        for (int i = listingCoinzContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingCoinzContainer.GetChild(i).gameObject);
        }

    }

    public void CloseRatingboardPanel()
    {
        for (int i = listingRatingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingRatingContainer.GetChild(i).gameObject);
        }

    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
        #endregion
    
}
