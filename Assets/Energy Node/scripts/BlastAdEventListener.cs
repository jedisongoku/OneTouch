using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAdEventListener : MonoBehaviour {

    private void OnEnable()
    {
        BlastAd.OnAdShow += OnBlastAdShowHandler;
        BlastAd.OnAdEnd += OnblastAdEndHandler;
    }   

    private void OnDestroy()
    {
        BlastAd.OnAdShow -= OnBlastAdShowHandler;
        BlastAd.OnAdEnd -= OnblastAdEndHandler;
    }

    private void OnblastAdEndHandler()
    {
        gameObject.SetActive(true);
    }

    private void OnBlastAdShowHandler()
    {
        gameObject.SetActive(false);
    }
}
