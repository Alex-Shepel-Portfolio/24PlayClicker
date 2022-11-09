using UnityEngine;
using System.Collections;
#if UNITY_IOS
using static Unity.Advertisement.IosSupport.ATTrackingStatusBinding;
#endif

public class ATTHelper : MonoBehaviour
{
#if UNITY_IOS && !UNITY_EDITOR
    private void Awake()
    {
        RequestAuthorizationAppTrackingTransparency();
    }
#endif

#if UNITY_IOS
    private void RequestAuthorizationAppTrackingTransparency()
    {
        if (IsTrackingNotDetermined())
        {
            Debug.Log("Request Tracking");
            RequestAuthorizationTracking();
        }
        StartCoroutine(LunchCallback());
    }

    private IEnumerator LunchCallback()
    {
        Debug.Log("Tracking Callback");
        while (IsTrackingNotDetermined())
        {
            yield return null;
        }
        Debug.Log("Tracking Status : " + GetAuthorizationTrackingStatus());
    }

    public static bool IsTrackingNotDetermined()
    {
        return GetAuthorizationTrackingStatus() == AuthorizationTrackingStatus.NOT_DETERMINED;
    }

    public static bool IsTrackingAllow()
    {
        return GetAuthorizationTrackingStatus() == AuthorizationTrackingStatus.AUTHORIZED;
    }
#endif
}