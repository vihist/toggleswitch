
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameMain : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Awake()
    {

        m_myGame = new MyGame();
        m_fWaitTime = 3.0F;

		StartCoroutine(OnTimer());  
    }

	private IEnumerator OnTimer()
    {
		while(!m_myGame.IsEnd())
		{
			m_myGame.NextTurn();

			foreach(MessageBox msgbox in m_myGame.m_ListMessageBox)
			{
				CheckDialog Dialog = MsgBox(msgbox.strTitile, msgbox.strContent, msgbox.arrOption);

				yield return StartCoroutine(Dialog.IsChecked());
			}

			yield return new WaitForSeconds(m_fWaitTime);
		}

		SceneManager.LoadScene ("EndScene");
		yield break;
    }

	private class CheckDialog
	{
		public GameObject dialog;
		public bool bCheck;
		public IEnumerator IsChecked()
		{
			while(!bCheck) 
			{
				yield return null; 
			}
			yield break;
		}
	}

	private CheckDialog MsgBox(string strTitle, string strContent, ArrayList arrOption)
	{
		GameObject UIRoot = GameObject.Find("Canvas");


		CheckDialog ckDialog = new CheckDialog ();

		GameObject dialog = Instantiate(Resources.Load(String.Format("EasyMenu/_Prefabs/Dialog_{0}Btn", 1)), UIRoot.transform) as GameObject;

		Text txTitle = dialog.transform.Find("Title").GetComponent<Text>();
		txTitle.text = strTitle;

		Text txContent = dialog.transform.Find("Content").GetComponent<Text>();
		txContent.text = strContent;

		ckDialog.dialog = dialog;
		ckDialog.bCheck = false;

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
    
					ckDialog.dialog = null;
					ckDialog.bCheck = true;
					
                });
		}

		return ckDialog;

    }

    private MyGame m_myGame;
	private float m_fWaitTime;
}
