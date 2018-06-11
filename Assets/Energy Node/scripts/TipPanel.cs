using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
//using DG.Tweening;
public class TipPanel : MonoBehaviour {
	
	// Use this for initialization
	bool canTick = true;
	void Start () {
		lb_notip = GameObject.Find ("lb_notip").GetComponentInChildren<Text> ();
		canTick = true;

		GameObject.Find ("tipTitle").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("askTip");
		GameObject.Find ("btnYes").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnyes");
		GameObject.Find ("btnNo").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnno");
	}
	
	// Update is called once per frame
	int n = 0;
	
	void Update () {
				if (n < 20) {
					n++;
					return;
				} else {
					n= 0;		
				}

		if (!canTick)
						return;
			
		if (GameData.getInstance ().tipRemain == 0) {
					
//			DateTime tnow = System.DateTime.Now;
			TimeSpan ts=new TimeSpan(50,0,0,0);  
			DateTime dt2=DateTime.Now.Subtract(ts); 
			long tcTime = dt2.Ticks/10000000;



			int tTimeLasts = (int)(tcTime - long.Parse(GameData.getInstance ().tickStartTime));
			

			int secondRemain = 300 - tTimeLasts;
			if (secondRemain <= 0) {
				secondRemain = 0;
				//count of;
				PlayerPrefs.SetInt ("tipRemain", 1);
				PlayerPrefs.SetString ("tipStart", "0");
				GameData.getInstance ().tipRemain = 1;
				GameData.getInstance ().tickStartTime = "0";
				GameData.getInstance().main.refreshView();
				checkUI();
				print ("startrefresh");
			}

				lb_notip.text = Localization.Instance.GetString("nextTip") + (secondRemain).ToString () + " seconds";		
			
		}
	}
	//	public delegate void PanelChangedEventHandler();
	//	public event PanelChangedEventHandler showPanel;
	bool isShowed;
	bool canShow = true;
	public void showTipPanel()
	{
		GameManager.getInstance ().playSfx ("click");
		showOrHideTipPanel ();
		
	}
	
	
	public void yesHandler(){
				if (!isShowed)
						return;
		GameManager.getInstance ().playSfx ("click");
		showOrHideTipPanel ();
		showTip ();
	}
	
	public void noHandler()
	{
		GameManager.getInstance ().playSfx ("click");
		showOrHideTipPanel ();
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
		GameData.getInstance().isLock = false;
				GameObject.Find ("btnRetryB").GetComponent<Button> ().interactable = true;
	}
	
		Text lb_notip;
		Button btnYes,btnNo;
	void checkUI(){
		
				btnYes = GameObject.Find ("btnYes").GetComponent<Button> ();
				btnNo = GameObject.Find ("btnNo").GetComponent<Button> ();
		//		print (GameData.getInstance ().tipRemain + "remain");
		if (GameData.getInstance ().tipRemain == 0) {
			
						lb_notip.enabled = true;
						btnYes.interactable = false;
			
		} else {
						btnYes.interactable = true;
						lb_notip.enabled = false;
		}
				if (GameData.getInstance ().isLock)
						GameObject.Find ("btnRetryB").GetComponent<Button> ().interactable = false;
	}
	
	public void showOrHideTipPanel(){
		if (!canShow)
			return;
		GameData.getInstance ().tickStartTime = PlayerPrefs.GetString("tipStart","0");
		// Add event handler code here
		if (!isShowed) {
//			if (showTween) {
//				showTween.Play ();
//				gameObject.transform.DOLocalMoveY(53,.3f).OnComplete(OnShowCompleted);
						ATween.MoveTo(gameObject, ATween.Hash("islocal",true,"y", 53, "time",1f,"easeType", "easeOutExpo", "oncomplete", "OnShowCompleted","oncompletetarget",this.gameObject));

						//						


				canShow = false;
				GameData.getInstance().isLock = true;
				//disable some UI;
				checkUI();
//			}
//			if(FadeOutTween)FadeOutTween.Play();
		} else {
//			if (hideTween) {
//				hideTween.Play();
				canShow = false;
//			}		
						ATween.MoveTo(gameObject, ATween.Hash("islocal",true,"y", 419, "time",1f,"easeType", "easeOutExpo", "oncomplete", "OnHideCompleted","oncompletetarget",this.gameObject));

//						gameObject.transform.DOLocalMoveY(419,.3f).OnComplete(OnHideCompleted);
//			if(FadeInTween)FadeInTween.Play();
		}


	}
	
	void showTip(){
		string[] tansArr = GameData.getInstance ().main.lvAnswerData;
		for (int i= 0; i< tansArr.Length-1; i++) {
			GameObject tNode = GameObject.Find("node_"+tansArr[i]);
						Text tNodeLabel = tNode.transform.Find("lb_tip").GetComponent<Text>();
						tNodeLabel.enabled = true;
						if(tNodeLabel.text== ""){
								tNodeLabel.text = (i+1).ToString();
			}else{
								tNodeLabel.text = tNodeLabel.text+","+(i+1).ToString();
			}
			

		}
		
		if(GameData.getInstance().tipRemain > 0){
			GameData.getInstance().tipRemain--;
			PlayerPrefs.SetInt("tipRemain",GameData.getInstance().tipRemain);
			GameData.getInstance().main.refreshView();
			//have not give a tip
			GameData.getInstance ().tickStartTime = PlayerPrefs.GetString("tipStart","0");
			if(GameData.getInstance().tickStartTime == "0"){
				canTick = false;		
//				long tcTime = System.DateTime.Now.Ticks;

				TimeSpan ts=new TimeSpan(50,0,0,0);  
				DateTime dt2=DateTime.Now.Subtract(ts); 
//				print (dt2.Ticks/10000000/3600);
				long tcTime = dt2.Ticks/10000000;

				PlayerPrefs.SetString("tipStart",tcTime.ToString());
				GameData.getInstance().tickStartTime = tcTime.ToString();
				//				print (tcTime+"tctime11");
				canTick = true;		
			}
		}
		
		if (GameData.getInstance ().tipRemain == 0) {
			canTick = true;
		} else {
			canTick = false;		
		}
		
				GameObject.Find ("btnTip").GetComponent<Button> ().interactable = false;
				print ("yes");
	}
	
}
