using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BlastAd : MonoBehaviour
{
#region Public Variables
    //Singleton reference
    public static BlastAd instance;
    //The time for the ad to be displayed until the exit button pops up
    public float displayTime = 3f;
    //Reference to the exit button
    public Transform exitButton;
    //Reference to the panel containing the ad
    public Transform adPanel;
    //If the ad is rewarded
    public bool isRewardedAd;
    //Reference to the video player
    public VideoPlayer videoPlayer;
    //Audio sources to enable and disable when Blast ad is playing
    public AudioSource[] nonVideoAudioSources;
#endregion

#region Private Variables
    //The incrementing time counter
    private float timeCounter;
    //if the ad is being shown
    private bool isShowingAd;
#endregion


   
    
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        if (videoPlayer.targetCamera == null)
            videoPlayer.targetCamera = Camera.main;
        ShowBlastAd();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isShowingAd)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter >= displayTime)
            {
                exitButton.gameObject.SetActive(true);
                isShowingAd = false;
            }
        }
        
	}
    //Show the Blast Ad
    public void ShowBlastAd()
    {
        if (videoPlayer.targetCamera == null)
            videoPlayer.targetCamera = Camera.main;
        adPanel.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        timeCounter = 0;
        isShowingAd = true;
        foreach (AudioSource source in nonVideoAudioSources)
            source.enabled = false;
        videoPlayer.Play();
    }

    public void HideBlastAd()
    {
        adPanel.gameObject.SetActive(false);
        if(isRewardedAd)
        {
            AdReward();
            isRewardedAd = false;
        }
        foreach (AudioSource source in nonVideoAudioSources)
        {
            source.enabled = true;
            source.Play();
        }            
    }

    public void OpenURL()
    {
        Application.OpenURL("https://app.adjust.com/m24ohje");
    }

    public void AdReward()
    {
        //Replace logic in this function to correctly reward the player
    }
}
