using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardInGameMenu : MonoBehaviour
{

    public Score player;

    public Text message;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging/creating");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard()
    {
        int score = (int)this.player.getScore();
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                   new StatisticUpdate{
                        StatisticName = "PlatformScore",
                        Value = score
                    }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        message.text = "Updated leaderboards!";
        Debug.Log("Updated leaderboards!");
    }
}
