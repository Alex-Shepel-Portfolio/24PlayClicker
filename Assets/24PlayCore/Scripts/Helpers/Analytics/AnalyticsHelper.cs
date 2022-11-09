using GameAnalyticsSDK;
using UnityEngine;

public static class AnalyticsHelper
{
    private static bool levelWasStarted;

    public static void Init()
    {
        GameAnalytics.Initialize();
        FacebookHelper.Initialize();
        Debug.Log(GetColoredMessage("Analytics - Init"));
    }

    public static void StartLevel()
    {
        levelWasStarted = true;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GetCurrentLevelName());
        Debug.Log(GetColoredMessage("Analytics - StartLevel:" + GetCurrentLevelIndex()));
    }

    public static void CompleteLevel()
    {
        if (!levelWasStarted)
            return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, GetCurrentLevelName());
        Debug.Log(GetColoredMessage("Analytics - CompleteLevel:" + GetCurrentLevelIndex()));
        IncrementLevel();
        levelWasStarted = false;
    }

    public static void FailLevel()
    {
        if (!levelWasStarted)
            return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, GetCurrentLevelName());
        Debug.Log(GetColoredMessage("Analytics - FailLevel:" + GetCurrentLevelIndex()));
        levelWasStarted = false;
    }

    private static string GetColoredMessage(string message)
    {
#if UNITY_EDITOR
        return $"<color=white>{message}</color>";
#else
        return message;
#endif
    }

    private static int GetCurrentLevelIndex()
    {
        return PlayerPrefs.GetInt("AH_GA_Level", 1);
    }

    private static string GetCurrentLevelName()
    {
        return string.Format("Level_{0}", GetCurrentLevelIndex());
    }

    private static void IncrementLevel()
    {
        int curLevel = GetCurrentLevelIndex();
        PlayerPrefs.SetInt("AH_GA_Level", curLevel + 1);
        PlayerPrefs.Save();
    }
}