using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public class UnityAds : MonoBehaviour {

    public static UnityAds instance;
    public bool rewardZone;
    public bool isAdShowing = false;
    private int adsCounter;
    private int blastAdsCounter;
    void Awake()
    {
        instance = this;

        if(Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize("2622181", false);
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Initialize("2622179", false);
        }
        else
        {
            Advertisement.Initialize("2622179", false);
        }
        
    }

    public void ShowAd(string zone = "")
    {
        blastAdsCounter++;
        if(blastAdsCounter==10)
        {
            BlastAd.instance.ShowBlastAd();
            blastAdsCounter = 0;
        }
        else
        {
#if UNITY_EDITOR
            //StartCoroutine(WaitForAd());
#endif
            isAdShowing = true;
            if (string.Equals(zone, ""))
                zone = null;
            else
                rewardZone = true;

            ShowOptions options = new ShowOptions();
            options.resultCallback = AdCallbackhandler;

            if (Advertisement.IsReady(zone))
            {
                Advertisement.Show(zone, options);
                Debug.Log("Show AD");
            }
        }
    }

    internal void IncreaseCounterAndShowAd()
    {
        adsCounter++;
        if (adsCounter % 3 == 0)
            ShowAd();
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                if (rewardZone)
                {

                    isAdShowing = false;
                }
                    
                    
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}
