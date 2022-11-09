using GameAnalyticsSDK;
using System.Collections;
using UnityEngine;

public class PlaytimeAnalytics : MonoBehaviour
{
    private const string PlaytimeKey = "Playtime";
    private const int SecondsInMinute = 60;
    private const int BenchmarkTimeInSeconds = 10 * SecondsInMinute;

    private IEnumerator Start()
    {
        var playtimeInSeconds = GetPlaytime();
        var delay = new WaitForSecondsRealtime(1f);
        while (playtimeInSeconds <= BenchmarkTimeInSeconds)
        {
            yield return delay;
            playtimeInSeconds++;
            SavePlaytime(playtimeInSeconds);
            if (playtimeInSeconds % SecondsInMinute == 0)
            {
                var minute = playtimeInSeconds / SecondsInMinute;
                GameAnalytics.NewDesignEvent("Playtime:" + minute);
            }
        }
    }

    private int GetPlaytime()
    {
        var seconds = PlayerPrefs.GetInt(PlaytimeKey, 0);
        return seconds;
    }

    private void SavePlaytime(int newTime)
    {
        PlayerPrefs.SetInt(PlaytimeKey, newTime);
        PlayerPrefs.Save();
    }
}
