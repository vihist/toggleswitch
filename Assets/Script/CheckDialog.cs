using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class CheckDialog : MonoBehaviour
{
	public void Initial(string strTitle, string strContent, ArrayList arrOption)
	{
		GameObject UIRoot = GameObject.Find("Background");

		dialog = Instantiate(Resources.Load(String.Format("EasyMenu/_Prefabs/Dialog_{0}Btn", arrOption.Count)), UIRoot.transform) as GameObject;
		dialog.transform.SetAsFirstSibling(); 

		Text txTitle = dialog.transform.Find("Title").GetComponent<Text>();
		txTitle.text = strTitle;

		Text txContent = dialog.transform.Find("Content").GetComponent<Text>();
		txContent.text = strContent;

		bCheck = false;

		for(int i=0; i<arrOption.Count; i++)
		{
			Option option = (Option) arrOption[i];
			Button Btn = dialog.transform.Find("Button"+i).GetComponent<Button>();

			Text txBtn = Btn.transform.Find("Text").GetComponent<Text>();
			txBtn.text = option.strDesc;

			Btn.onClick.AddListener ( delegate () 
				{
					Debug.Log("OnClick");
					option.delegOnBtnClick();

					Destroy(dialog);

					dialog = null;
					bCheck = true;

				});
		}
			
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

}