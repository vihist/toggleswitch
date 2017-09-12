
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

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

    public void Awake()
    {
        m_myGame = new MyGame();
		m_myGame.m_delegMsgBox = this.MsgBox;

        Invoke("OnTimer", 1.0F);  //2秒后，没0.3f调用一次
    }

    private void OnTimer()
    {
        try
        {
            Debug.Log("OnTimer");

            m_myGame.NextTurn();

            //Invoke("OnTimer", 1.0F);
        }
        catch (EndGameException )
        {
            SceneManager.LoadScene("EndScene");
            return;
        }

    }

    public void OnClick()
    {
        Debug.Log("OnClick");

    }

	public void MsgBox(string strTitle, string strContent, ArrayList arrOption)
	{
		GameObject UIRoot = GameObject.Find("Canvas");


		GameObject dialog = Instantiate(Resources.Load(String.Format("EasyMenu/_Prefabs/Dialog_{0}Btn", arrOption.Count)), UIRoot.transform) as GameObject;

		Text txTitle = dialog.transform.Find("Title").GetComponent<Text>();
		txTitle.text = strTitle;

		Text txContent = dialog.transform.Find("Content").GetComponent<Text>();
		txContent.text = strContent;

		for(int i=0; i<arrOption.Count; i++)
		{
			MyGame.MsgBox msgBox = (MyGame.MsgBox) arrOption[i];
			Button Btn = dialog.transform.Find("Button"+i).GetComponent<Button>();

			Text txBtn = Btn.transform.Find("Text").GetComponent<Text>();
			txBtn.text = msgBox.strDesc;

			Btn.onClick.AddListener ( delegate () 
				{
					msgBox.delegOnBtnClick();
				}
			);
		}
	}

    private MyGame m_myGame;
}
