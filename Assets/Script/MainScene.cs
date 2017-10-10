
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;

public class MainScene : MonoBehaviour
{
	void Awake()
	{
		m_fWaitTime = 3.0F;
		StartCoroutine(OnTimer());  
	}

	// Use this for initialization
	void Start ()
    {
		SceneManager.LoadSceneAsync("TxScene", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update ()
    {
		OnRefreshData ();
		OnKeyBoard ();
	}

	public void OnValueChanged1(bool bCheck)
	{
		Debug.Log("toggle1" + bCheck);

		if (bCheck)
		{
			SceneManager.UnloadSceneAsync("CtScene");
			SceneManager.LoadSceneAsync("TxScene", LoadSceneMode.Additive);
		}
	}

	public void OnValueChanged2(bool bCheck)
	{
		Debug.Log("toggle2" + bCheck);

		if(bCheck)
		{
			SceneManager.UnloadSceneAsync("TxScene");
			SceneManager.LoadSceneAsync("CtScene", LoadSceneMode.Additive);
		}
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
					GameFrame.GetInstance().OnSave();
					Destroy(dialog);
				});

			Button btnQuit = dialog.transform.Find("Quit").GetComponent<Button>();
			btnQuit.onClick.AddListener ( delegate () 
				{
					GameFrame.GetInstance().OnQuit();
				});
		} 
	}

	private IEnumerator OnTimer()
	{
		while(!Global.GetMyGame().IsEnd())
		{
			Global.GetMyGame().NextTurn();

			for(int i=0; i<Global.GetMyGame().m_ListMessageBox.Count; i++)
			{
				MessageBox msgbox = Global.GetMyGame().m_ListMessageBox[i];
				CheckDialog Dialog = new CheckDialog (msgbox.strTitile, msgbox.strContent, msgbox.arrOption);

				yield return StartCoroutine(Dialog.IsChecked());

				Global.GetMyGame().m_ListMessageBox.InsertRange(i+1, msgbox.m_listNext);
			}

			yield return new WaitForSeconds(m_fWaitTime);
		}

		GameFrame.GetInstance ().OnEnd();
		yield break;
	}


	private float m_fWaitTime;
}
