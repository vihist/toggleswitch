
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;

public class GameMain : MonoBehaviour
{
	void Awake()
	{

		m_myGame = new MyGame();
		m_fWaitTime = 3.0F;

		StartCoroutine(OnTimer());  
	}

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		OnRefreshData ();
		OnKeyBoard ();
	}

	private void OnRefreshData()
	{
		GameObject UIRoot = GameObject.Find("ResInfo");

		Text txTitle = UIRoot.transform.Find("TX").GetComponent<Text>();
		txTitle.text = "111";
	}

	private void OnKeyBoard()
	{
		if (Input.GetKeyDown (KeyCode.Escape))  
		{  
			GameObject UIRoot = GameObject.Find("Canvas");
			GameObject dialog = Instantiate(Resources.Load("EasyMenu/_Prefabs/Dialog_Esc"), UIRoot.transform) as GameObject;

			Button btnSave = dialog.transform.Find("Save").GetComponent<Button>();
			btnSave.onClick.AddListener ( delegate () 
				{
					OnSave();
					Destroy(dialog);
				});

			Button btnQuit = dialog.transform.Find("Quit").GetComponent<Button>();
			btnQuit.onClick.AddListener ( delegate () 
				{
					Application.Quit();
				});
		} 
	}

	private void OnSave()
	{
		if (!Directory.Exists(@".\save"))
		{
			Directory.CreateDirectory(@".\save");
		}

		string json = JsonUtility.ToJson (m_myGame.GetGameData ());
		File.WriteAllText (@"\save\game.save", json);
	}

	private IEnumerator OnTimer()
	{
		while(!m_myGame.IsEnd())
		{
			m_myGame.NextTurn();

			for(int i=0; i<m_myGame.m_ListMessageBox.Count; i++)
			{
				MessageBox msgbox = m_myGame.m_ListMessageBox[i];
				CheckDialog Dialog = MsgBox(msgbox.strTitile, msgbox.strContent, msgbox.arrOption);

				yield return StartCoroutine(Dialog.IsChecked());

				m_myGame.m_ListMessageBox.InsertRange(i+1, msgbox.m_listNext);
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
