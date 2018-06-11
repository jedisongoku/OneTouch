using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DG.Tweening;
public class EneryNode : MonoBehaviour {

		// Use this for initialization

		//	public Vector3 myPos;

		public static Image currentNode;
		void Start () {



		}

		// Update is called once per frame
		void Update () {

		}


		/// <summary>
		/// the UI event for click on a node.
		/// </summary>
		int state = 0;
		public void OnClick()
		{


				if (GameData.getInstance ().isLock)
						return;
				if (GameData.getInstance ().isfail)
						return;

				Image cSp = gameObject.GetComponent<Image>();


				if(currentNode == null || cSp != currentNode){

						//get link name
						GameObject tnode0;GameObject tnode1;
						tnode0 = transform.parent.gameObject;
						if(currentNode){
								tnode1 = currentNode.transform.parent.gameObject;
								int tnodeId0 = int.Parse(tnode0.name.Split("_"[0])[1]);
								int tnodeId1 = int.Parse(tnode1.name.Split("_"[0])[1]);
								string tlinklineName = "linkLine"+"_"+Mathf.Min(tnodeId0,tnodeId1)+"_"+Mathf.Max(tnodeId0,tnodeId1);
								GameObject tLinkLine = GameObject.Find(tlinklineName);
								if(tLinkLine){

										//last node turn to blue
										EneryLink enerylink = tLinkLine.GetComponentInChildren<EneryLink>();
										if(enerylink.state == 1 || enerylink.state == 2){
												enerylink .changeState(2);
										}else{
												enerylink .changeState(1);
												//light the node only when can link a new line
												changeState(2);//turn green
												if(currentNode){

														currentNode.gameObject.GetComponent<EneryNode>().changeState(1);
														//active node
														currentNode = gameObject.GetComponentInChildren<Image>();//pName.Split("_"[0])[1];
												}

												//link a useful line

												GameData.getInstance().nLink --;
					
												if(GameData.getInstance().nLink == 0){
														print("win");
														//fire event;	
														GameData.getInstance().main.gameWin();
												}

										}
								}
						}else{

								//first node
								changeState(1);
								//active node
								currentNode = gameObject.GetComponentInChildren<Image>();//pName.Split("_"[0])[1];

						}


				}




		}
		/// <summary>
		/// Changes the color when touch a node
		/// </summary>
		/// <param name="state_">State.</param>
		public void changeState(int state_){
				state = state_;
				Color tcolor = GetComponent<Image> ().color;
				switch (state) {
				case 0:
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(1,1,1,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));

						//						GetComponent<Image> ().DOColor (new Color(1,1,1,1),.3f);
						break;
				case 1:
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(.2f,.6f,.8f,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));


						break;
				case 2:
						ATween.ValueTo (gameObject, ATween.Hash ("from", tcolor, "to",new Color(0,1,0,1), "time", .3f, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject));


						break;
				}
		}


		/// <summary>
		/// the update process for color tween
		/// </summary>
		/// <param name="value">Value.</param>
		void OnUpdateTween(Color value)

		{

				GetComponent<Image>().color = value;
		}


}
