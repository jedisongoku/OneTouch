using UnityEngine;
using System.Collections;
//using DG.Tweening;
using UnityEngine.UI;
public class EneryLink : MonoBehaviour {

	// Use this for initialization
	public int state;
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


		/// <summary>
		/// Changes the state when link a line
		/// </summary>
		/// <param name="state_">State.</param>
	public void changeState(int state_){
		state = state_;
				Color tcolor = GetComponent<Image> ().color;
				switch (state) {
				case 0:
						
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(1,1,1,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));
						break;
				case 1:
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(.2f,.6f,.8f,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));

						break;
				case 2:
						GameData.getInstance ().isfail = true;
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(1,0,0,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));

						break;
				}
	}

		/// <summary>
		/// for tween the color
		/// </summary>
		/// <param name="value">Value.</param>
		void OnUpdateTween(Color value)

		{

				GetComponent<Image>().color = value;
		}
}
