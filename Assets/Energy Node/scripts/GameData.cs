using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData  {

	// Use this for initialization

	public int nLink = 0; //check in game.When nlink = 0.All the lines linked,so win.
	public int levelPassed = 0;//how much level you passed
	public int cLevel = 0;//currect level
	public int bestScore = 0;//bestscore for level
	public int isSoundOn = 0;//whether game music is on
	public int isSfxOn = 0;//whether the game sound effect is on
	public static bool isTrial;//not used
	public static string lastWindow = "";//not used

	public int tipRemain = 0;//how much tip you remain

	public MainScript main;//the mainscript instance of the game
	public static int totalLevel = 260;//total levels


	

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static GameData instance;
	public static GameData getInstance(){
		if (instance == null) {
			instance = new GameData();
//			PlayerPrefs.DeleteAll ();
		}
		return instance;
	}

	public bool isWin = false;//check if win
	public bool isLock = false;//check if game ui can touch or lock and wait
	public string tickStartTime = "0";//game count down.
	public List<int>lvStar = new List<int>(260);//level stars you got for each level
	public bool isfail = false;//whether the game failed

		/// <summary>
		/// Always uses for initial or reset to start a new level.
		/// </summary>
	public void resetData(){
		isLock = false;
		isWin = false;
		isfail = false;
//		levelPassed = PlayerPrefs.GetInt ("levelPass", 0);
//		Debug.Log ("levelpassed=" + levelPassed);
		tipRemain = PlayerPrefs.GetInt ("tipRemain", 3);
		tickStartTime = PlayerPrefs.GetString ("tipStart", "0");
	}


		/// <summary>
		/// Gets the system laguage.
		/// </summary>
		/// <returns>The system laguage.</returns>
		public int GetSystemLaguage(){
				int returnValue = 0;
				switch (Application.systemLanguage) {
				case SystemLanguage.Chinese:
						returnValue = 1;
						break;
				case SystemLanguage.ChineseSimplified:
						returnValue = 1;
						break;
				case SystemLanguage.ChineseTraditional:
						returnValue = 1;
						break;
				default:
						returnValue = 0;
						break;

				}
				returnValue = 0;//test
				return returnValue;
		}


}
