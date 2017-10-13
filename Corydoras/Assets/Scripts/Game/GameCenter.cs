using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using GlobalDefines;


public class GameCenter : MonoBehaviour
{
    bool isGameCenterSuccess = false;
    public string userInfo;
    public GameDirector director;
    // Use this for initialization
    void Start()
    {
        director = GetComponent<GameDirector>();
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 初始化gamecenter后的回调
    private void HandleAuthenticated(bool success)
    {
        isGameCenterSuccess = success;
        if (success) {
            userInfo = "Username: " + Social.localUser.userName +
                "\nUser ID: " + Social.localUser.id +
                "\nIsUnderage: " + Social.localUser.underage;
           // Debug.Log(userInfo);
            //director.UpdateAllGameCenterData();
        }
        else {
            //Debug.Log("GameCenter Init failed");
        }
    }

    private void HandleAchievementsLoaded(IAchievement[] achievements)
    {
        //Debug.Log("HandleAchievementsLoaded");
    }

    private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
    {
        //Debug.Log("HandleAchievementDescriptionsLoaded");
    }

    private void HandleFriendsLoaded(bool success)
    {
        //Debug.Log("HandleFriendsLoaded: success = " + success);
    }

    //上传排行榜分数  
    private void HandleScoreReported(bool success)
    {
        //Debug.Log("HandleScoreReported: success = " + success);
    }
    //设置 成就  
    private void HandleProgressReported(bool success)
    {
        //Debug.Log("HandleProgressReported: success = " + success);
    }

    public void Login()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
		return;
#elif UNITY_IOS || UNITY_IPHONE
        Social.localUser.Authenticate(HandleAuthenticated);
#endif
	}

    public void ShowGameCenter()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return;
#elif UNITY_IOS || UNITY_IPHONE
        if (isGameCenterSuccess && Social.localUser.authenticated) {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Login();
        }
#endif
    }

    public void UpdateScore(int score)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
		return;
#elif UNITY_IOS || UNITY_IPHONE
        if (Social.localUser.authenticated) {
            Social.ReportScore(score, GameCenterKey.Ladder, HandleScoreReported);
        }
#endif
	}

    public void UpdateReportProgress(string achieveId, int score, int totalScore)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return;
#elif UNITY_IOS || UNITY_IPHONE
		if (Social.localUser.authenticated) {
            Social.ReportProgress(achieveId, (double)score / (double)totalScore * 100, HandleProgressReported);
        }
#endif
    }
}
