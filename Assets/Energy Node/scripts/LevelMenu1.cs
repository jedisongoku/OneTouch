using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.SceneManagement;
/// <summary>
/// Level menu.
/// </summary>
public class LevelMenu1 : MonoBehaviour {

		// Use this for initialization

		GameObject listItemg;

		void Start () {
				GameData.getInstance ().resetData();
				initView ();
		}

		// Update is called once per frame
		void Update () {

		}


		public GameObject levelButton;//each level button
		public GameObject dot;//level page dots

		int page = 0; // current page
		int pages = 1;//total pages(calc automatically)
		int perpage = 25;//each page how much level icon you want to show
		List<GameObject> gContainer;//the level buttons container
		List<GameObject> pageDots;//the level page dots contaner.
		int gap = 140;//page gap
		public Image mask;//the fade mask

		/// <summary>
		/// Inits the view.draw page button.regist the click handler
		/// </summary>
		void initView(){

				GameObject.Find ("confirm").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("continue");


				pageDots = new List<GameObject> ();


				pages = Mathf.FloorToInt (GameData.totalLevel / perpage);
				for (int i = 0; i <= pages; i++) {
						GameObject tdot = Instantiate (dot, dot.transform.parent) as GameObject;
						tdot.SetActive (true);
						pageDots.Add (tdot);
						tdot.name = "dot_" + i;

				}

				setpageDot ();
				fadeOut ();

				gContainer = new List<GameObject>();
				gContainer.Add (levelButton.transform.parent.gameObject);
				//		levelButton.GetComponent<RectTransform> ().localScale = Vector3.one;
				Transform container = levelButton.transform.parent;
				container.transform.localScale = Vector3.one;

				for (int i = perpage; i < GameData.totalLevel; i+=perpage) {
						GameObject tgroup = Instantiate (levelButton.transform.parent.gameObject,levelButton.transform.parent.position, Quaternion.identity) as GameObject;
						tgroup.transform.Translate (gap*(i+1), 0, 0);
						gContainer.Add (tgroup);

						tgroup.transform.parent = levelButton.transform.parent.gameObject.transform.parent;
				}


				for (int i = 0; i < GameData.totalLevel; i++) {
						GameObject tbtn = Instantiate (levelButton, Vector3.zero, Quaternion.identity) as GameObject;

						int tContainerNo = Mathf.FloorToInt (i / perpage);
						tbtn.transform.parent = gContainer[tContainerNo].transform;
						//			gContainer[tContainerNo].GetComponent<RectTransform> ().localScale = Vector3.one;
						tbtn.SetActive (true);

						tbtn.transform.localScale = new Vector3(2,2,1);
						tbtn.GetComponentInChildren<Text> ().text = (i+1).ToString ();
						tbtn.transform.parent.localScale = Vector3.one;



						//set up view of stars and lock buttons 
						Text ttext = tbtn.GetComponentInChildren<Text> ();
						if (i >= GameData.getInstance ().levelPassed+1 && i > 0) {

								ttext.enabled = false;




						} else {
								if (GameData.getInstance ().lvStar.Count > i) {
										
										int starCount = GameData.getInstance ().lvStar [i];

//										print (i + "== " + starCount);

										if (GameData.getInstance ().lvStar.Count > i) {
												for (int j = 1; j <= starCount; j++) {
														ttext.transform.parent.Find ("star" + j).GetComponent<Image> ().enabled = true;
												}
										}
								}
								tbtn.GetComponent<Button> ().onClick.AddListener (() => clickLevel (tbtn));
								ttext.gameObject.transform.parent.Find ("lock").GetComponent<Image> ().enabled = false;
						}

				}

				GameObject.Find ("txtScore").GetComponent<Text> ().text = Localization.Instance.GetString("totalScore")+" " + GameData.getInstance ().bestScore;
		}


		/// <summary>
		/// Clicks the dot.You may change page.
		/// </summary>
		/// <param name="tdot">Tdot.</param>
		public void clickDot(GameObject tdot){
				int tdotIndex = int.Parse(tdot.name.Substring (4, tdot.name.Length - 4));
				page = tdotIndex;
				canmove = false;


				ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));



		}	

		/// <summary>
		/// page change tween finished
		/// </summary>
		void dotclicked(){
				canmove = true;
				setpageDot ();
		}

		/// <summary>
		/// Clicks the level button,you will enter a new level
		/// </summary>
		/// <param name="tbtn">Tbtn.</param>
		void clickLevel(GameObject tbtn){
				GameData.getInstance().cLevel = int.Parse(tbtn.GetComponentInChildren<Text>().text)-1;

				fadeIn ("Game");

		}


		/// <summary>
		/// init and refresh the dot state and numbers
		/// </summary>
		void setpageDot(){
				for (int i = 0; i < pageDots.Count; i++) {
						pageDots [i].GetComponent<Image> ().color = new Color (1, 1, 1, .5f);
				}
				pageDots [page].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}


		/// <summary>
		/// a button touches to continues the level directly.
		/// </summary>
		public void continueLevel(){
				int tLastLevel = GameData.getInstance ().levelPassed;
				if (tLastLevel < GameData.totalLevel) {
						GameData.getInstance ().cLevel = tLastLevel;
				} else {
						GameData.getInstance().cLevel = GameData.totalLevel-1;
				}
				fadeIn ("Game");
		}

		/// <summary>
		/// Backs the main title.
		/// </summary>
		public void backMain(){
				GameManager.getInstance ().playSfx ("click");
				fadeIn ("MainMenu");
		}

		public void loadGameScene(){
				//		GameObject.Find ("loading").GetComponent<dfPanel> ().IsVisible = true;
				SceneManager.LoadScene("Game"); 
		}
		public void loadMainScene(){
				//		GameObject.Find ("loading").GetComponent<dfPanel> ().IsVisible = true;
				SceneManager.LoadScene("MainMenu"); 
		}


		bool canmove = true;//lock the buttons when tween is playing
		/// <summary>
		/// page turn right
		/// </summary>
		public void GoRight(){
				if (!canmove)
						return;
				if (page < pages) {

						page++;
						canmove = false;

						ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash( "islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));

//						gContainer[0].transform.parent.DOLocalMoveX (-gContainer[page].transform.localPosition.x,.3f).OnComplete(()=>{
//								canmove = true;
//								setpageDot ();
//						});

				}
		}
		/// <summary>
		/// page turn left
		/// </summary>
		public void GoLeft(){
				if (!canmove)
						return;
				if (page > 0) {

						page--;
						canmove = false;

//						gContainer[0].transform.parent.DOLocalMoveX (-gContainer[page].transform.localPosition.x,.3f).OnComplete(()=>{
//								canmove = true;
//								setpageDot ();
//						});
						ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));


				}
		}

		/// <summary>
		/// Fade out camera
		/// </summary>
		void fadeOut(){
				mask.gameObject.SetActive (true);
				mask.color = Color.black;
//				mask.DOFade (0, 1).OnComplete (() => {
//						mask.gameObject.SetActive (false);
//				});
				ATween.ValueTo (mask.gameObject, ATween.Hash ("from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver","oncompletetarget",this.gameObject));

		}
		/// <summary>
		/// Fade in camera
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		void fadeIn(string sceneName){
				if (mask.IsActive())
						return;
				mask.gameObject.SetActive (true);
				mask.color = new Color(0,0,0,0);
//				mask.DOFade (1, 1).OnComplete (() => {
//						//			mask.gameObject.SetActive (false);
//						SceneManager.LoadScene(sceneName);
//				});

				ATween.ValueTo (mask.gameObject, ATween.Hash ("from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName,"oncompletetarget",this.gameObject));

		}


		/// <summary>
		/// when Fadein over.
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		void fadeInOver(string sceneName){
				SceneManager.LoadScene(sceneName);
		}

		/// <summary>
		/// when fade out over
		/// </summary>
		void fadeOutOver(){
				mask.gameObject.SetActive (false);
		}

		/// <summary>
		/// tween update event
		/// </summary>
		/// <param name="value">Value.</param>
		void OnUpdateTween(float value)

		{

				mask.color = new Color(0,0,0,value);
		}



}
