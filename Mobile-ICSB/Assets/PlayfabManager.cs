using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{

    public GameObject rowPrefab;
    public Transform rowsParent;

    public Text message;
    public InputField emailInput;
    public InputField passwordInput;
    public InputField usernameInput;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("account logged/created");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging/creating");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
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
        Debug.Log("Updated leaderboards!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(var item in result.Leaderboard)
        {

            GameObject newGameObject = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGameObject.GetComponentsInChildren<Text>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }


    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            Username = usernameInput.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = usernameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplaySuccess, OnError);
        message.text = "Logged in!";
    }

    void OnDisplaySuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated displayName!");
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        message.text = "Registered and logged in!";
    }
}
