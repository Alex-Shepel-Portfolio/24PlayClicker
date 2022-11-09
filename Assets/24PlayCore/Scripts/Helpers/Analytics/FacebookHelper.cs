using Facebook.Unity;
using UnityEngine;

public class FacebookHelper
{
    public static void Initialize()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            InitCallback();
        }
    }

    private static void InitCallback()
    {
        if (FB.IsInitialized)
        {
#if UNITY_IOS && !UNITY_EDITOR
            FB.Mobile.SetAdvertiserTrackingEnabled(ATTHelper.IsTrackingAllow());
#endif
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private static void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
