using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.SceneManagement;
public class WinPanel : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
				GameObject.Find ("txtComplete").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("levelComplete");

				GameObject.Find ("btnRetry").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnRetry");
				GameObject.Find ("btnConti").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("continue");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//	public delegate void PanelChangedEventHandler();
	//	public event PanelChangedEventHandler showPanel;
	bool isShowed;
	bool canShow = true;
	//continue
	public void OnContinueEventHandler()
	{
				if (!isShowed)
						return;
		GameManager.getInstance ().playSfx ("click");
		showHidePanel ();

		if (GameData.getInstance ().cLevel < GameData.totalLevel-1) {
			GameData.getInstance ().cLevel++;
		} else {
			GameData.getInstance ().cLevel = 0;	
		}

        UnityAds.instance.IncreaseCounterAndShowAd();
		
	}
	
	public void OnRetryEventHandler()
	{
		GameManager.getInstance ().playSfx ("click");
		showHidePanel ();
		
	}

	public void OnShowCompleted()
	{
		// Add event handler code here
		//		print ("showOver");
		isShowed = true;
		canShow = true;
	}
	
	public void OnHideCompleted()
	{
		//		print ("hideOver");	
		isShowed = false;
		canShow = true;
				if (GameData.getInstance ().isWin) {
						//						SceneManager.LoadScene ("Game");
						SceneManager.LoadScene ("Game");
				}
	
	}
	
	
	public void showHidePanel(int nStarGet = 0){
		if (!canShow)
			return;
		
		// Add event handler code here
		if (!isShowed) {
//			if (showTween) {
//				showTween.Play ();
						ATween.MoveTo(gameObject, ATween.Hash("islocal",true,"y", 53, "time",1f,"easeType", "easeOutExpo", "oncomplete", "OnShowCompleted","oncompletetarget",this.gameObject));

//				gameObject.transform.DOLocalMoveY(53,.3f).OnComplete(OnShowCompleted);
				GameObject.Find ("btnRetryB").GetComponent<Button> ().interactable = false;
				canShow = false;
				
				GameObject starScore = gameObject.transform.Find("star"+nStarGet.ToString()).gameObject;
						starScore.GetComponent<Image>().enabled = true;
				
//			}
		
		} else {
//			if (hideTween) {
//				hideTween.Play();
//				gameObject.transform.DOLocalMoveY(419,.3f).OnComplete(OnHideCompleted);
				ATween.MoveTo(gameObject, ATween.Hash("islocal",true,"y", 419, "time",1f,"easeType", "easeOutExpo", "oncomplete", "OnHideCompleted","oncompletetarget",this.gameObject));
				canShow = false;
//			}		
		
		}



	}
}
 