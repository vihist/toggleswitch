using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

class CheckDialog : MonoBehaviour
{
	public void Initial(string strTitle, string strContent, ArrayList arrOption)
	{
        GameObject UIRoot = GameObject.Find("Canvas").transform.Find("Background").gameObject;


        dialog = Instantiate(Resources.Load(String.Format("EasyMenu/_Prefabs/Dialog_{0}Btn", arrOption.Count)), UIRoot.transform) as GameObject;
		dialog.transform.SetAsFirstSibling(); 

		Text txTitle = dialog.transform.Find("Title").GetComponent<Text>();
		txTitle.text = strTitle;

		Text txContent = dialog.transform.Find("Content").GetComponent<Text>();
		txContent.text = strContent;

		bCheck = false;

		OptionAdaptor opAdaptor = new OptionAdaptor (arrOption, this);

		for (int i = 0; i < opAdaptor.GetNum (); i++) 
		{
			Button Btn = dialog.transform.Find("Button"+i).GetComponent<Button>();

			Btn.transform.Find ("Text").GetComponent<Text> ().text = opAdaptor.GetText (i);
			Btn.onClick.AddListener (opAdaptor.GetDelegate(i));
		}

//		int i = 0;
//		for(; i<arrOption.Count; i++)
//		{
//			Option option = (Option) arrOption[i];
//			Button Btn = dialog.transform.Find("Button"+i).GetComponent<Button>();
//
//			Text txBtn = Btn.transform.Find("Text").GetComponent<Text>();
//			txBtn.text = option.strDesc;
//
//			Btn.onClick.AddListener ( delegate () 
//				{
//					int t = i;
//					Debug.Log("OnClick"+t.ToString());
//					option.delegOnBtnClick(t);
//
//					Destroy(dialog);
//
//					dialog = null;
//					bCheck = true;
//
//				});
//		}
			
	}

	public IEnumerator IsChecked()
	{
		while(!bCheck) 
		{
			yield return null; 
		}
		yield break;
	}

	private GameObject dialog;
	private bool bCheck;

	class OptionAdaptor
	{
		public OptionAdaptor(ArrayList arrOption, CheckDialog dialog)
		{
			this.arrOption = arrOption;
			this.dialog = dialog;
		}

		public int GetNum()
		{
			return arrOption.Count;
		}

		public String GetText(int i)
		{
			Option option = (Option) arrOption[i];
			return option.strDesc;
		}

		public UnityAction GetDelegate(int i)
		{
			return new UnityAction (delegate () {
				Debug.Log ("OnClick" + i.ToString ());
				Option option = (Option)arrOption [i];
				option.delegOnBtnClick (i);
				Destroy (dialog.dialog);
				dialog.dialog= null;
				dialog.bCheck = true;
			});
		}

		ArrayList arrOption;
		CheckDialog dialog;
	}

}