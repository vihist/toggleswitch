
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
		OnUiInit ();

		m_currSceneName = "TianXScene";
        m_isEmperorShow = false;

        SceneManager.LoadSceneAsync (m_currSceneName, LoadSceneMode.Additive);

		GameObject.Find ("Canvas").transform.Find ("Emperor").Find ("name").GetComponent<Text> ().text = Global.GetGameData ().emperor.GetName ();

	}
	
	// Update is called once per frame
	void Update ()
    {
		OnRefreshData ();
		OnKeyBoard ();
	}

	public void OnValueChanged1(bool bCheck)
	{
		//Debug.Log("toggle1" + bCheck);

		if (bCheck)
		{
			SceneManager.UnloadSceneAsync(m_currSceneName);

			m_currSceneName = "TianXScene";
			SceneManager.LoadSceneAsync(m_currSceneName, LoadSceneMode.Additive);
		}
	}

	public void OnValueChanged2(bool bCheck)
	{
		//Debug.Log("toggle2" + bCheck);

		if(bCheck)
		{
			SceneManager.UnloadSceneAsync(m_currSceneName);

			m_currSceneName = "ChaoTScene";
			SceneManager.LoadSceneAsync(m_currSceneName, LoadSceneMode.Additive);
		}
	}

	public void OnValueChanged3(bool bCheck)
	{
		if(bCheck)
		{
			SceneManager.UnloadSceneAsync(m_currSceneName);

			m_currSceneName = "HouGScene";
			SceneManager.LoadSceneAsync(m_currSceneName, LoadSceneMode.Additive);
		}
	}

    public void OnValueChanged4(bool bCheck)
    {
        if (bCheck)
        {
            SceneManager.UnloadSceneAsync(m_currSceneName);

            m_currSceneName = "StaticScene";
            SceneManager.LoadSceneAsync(m_currSceneName, LoadSceneMode.Additive);
        }
    }

    public void onEmperorButtonClick()
    {
        Debug.Log("onEmperorButtonClick");

        if (!m_isEmperorShow)
        {
            Transform UIRoot = GameObject.Find("Canvas").transform.Find("Emperor");
            GameObject dialog = Instantiate(Resources.Load("EasyMenu/_Prefabs/Dialog_Emperor"), UIRoot) as GameObject;
            dialog.transform.SetAsFirstSibling();

            m_isEmperorShow = true;
        }
        else
        {
            GameObject dialog = GameObject.Find("Canvas").transform.Find("Emperor").Find("Dialog_Emperor(Clone)").gameObject;
            Destroy(dialog);

            m_isEmperorShow = false;
        }
    }

    private void OnRefreshData()
	{
		GameObject UIRoot = GameObject.Find("ResInfo");

		UIRoot.transform.Find ("TM").transform.Find ("value").GetComponent<Text> ().text = Global.GetGameData ().tm.ToString();
		UIRoot.transform.Find ("FK").transform.Find ("value").GetComponent<Text> ().text = Global.GetGameData ().fk.ToString();
		UIRoot.transform.Find ("WB").transform.Find ("value").GetComponent<Text> ().text = Global.GetGameData ().wb.ToString();
        UIRoot.transform.Find("Time").GetComponent<Text>().text = Global.GetGameData().m_Date.ToString();


		Transform dialog = GameObject.Find("Canvas").transform.Find("Emperor").Find("Dialog_Emperor(Clone)");
		if (dialog != null) 
		{
			dialog.Find ("Age").Find ("Value").GetComponent<Text> ().text = Global.GetGameData ().emperor.age.ToString();
			dialog.Find ("Heath").Find("Slider").GetComponent<Slider> ().value = Global.GetGameData ().emperor.heath;
		}
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
					GameFrame.GetInstance().OnReturn();
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
     

                CheckDialog Dialog = GameObject.Find("Canvas").transform.Find("Background").gameObject.AddComponent<CheckDialog> ();
				Dialog.Initial (msgbox.strTitile, msgbox.strContent, msgbox.arrOption);

				yield return StartCoroutine(Dialog.IsChecked());

				Global.GetMyGame().m_ListMessageBox.InsertRange(i+1, msgbox.m_listNext);
			}

			yield return new WaitForSeconds(m_fWaitTime);
		}

		GameFrame.GetInstance ().OnEnd();
		yield break;
	}

	private void OnUiInit()
	{
		GameObject ResInfo = GameObject.Find("ResInfo");

		ResInfo.transform.Find("TM").GetComponent<Text>().text = UIFrame.GetUiDesc ("TM");
		ResInfo.transform.Find("FK").GetComponent<Text>().text = UIFrame.GetUiDesc ("FK");
		ResInfo.transform.Find("WB").GetComponent<Text>().text = UIFrame.GetUiDesc ("WB");

		GameObject TogglePanel = GameObject.Find ("TogglePanel");
		TogglePanel.transform.Find("TianX").transform.Find("Label").GetComponent<Text>().text = UIFrame.GetUiDesc ("TianX");
		TogglePanel.transform.Find("ChaoT").transform.Find("Label").GetComponent<Text>().text = UIFrame.GetUiDesc ("ChaoT");
        TogglePanel.transform.Find("HouG").transform.Find("Label").GetComponent<Text>().text = UIFrame.GetUiDesc("HouG");
		TogglePanel.transform.Find("Static").transform.Find("Label").GetComponent<Text>().text = UIFrame.GetUiDesc("Static");
    }


	private float m_fWaitTime;
	private String m_currSceneName;
    private bool m_isEmperorShow;
}
