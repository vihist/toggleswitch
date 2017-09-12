
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

    public void Awake()
    {
        m_ListDialog = new List<GameObject>();

        m_myGame = new MyGame();
		m_myGame.m_delegPopMsgBox = this.MsgBox;

        Invoke("OnTimer", 1.0F);  //2秒后，没0.3f调用一次
    }

    private void OnTimer()
    {
        try
        {
            m_myGame.NextTurn();
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
			MyGame.Option option = (MyGame.Option) arrOption[i];
			Button Btn = dialog.transform.Find("Button"+i).GetComponent<Button>();

			Text txBtn = Btn.transform.Find("Text").GetComponent<Text>();
			txBtn.text = option.strDesc;

			Btn.onClick.AddListener ( delegate () 
				{
                    option.delegOnBtnClick();

                    m_ListDialog.Remove(dialog);
                    Destroy(dialog);
    
                    if (m_ListDialog.Count == 0)
                    {
                        Invoke("OnTimer", 1F);
                    }
                }
			);
		}

        m_ListDialog.Add(dialog);

    }

    private MyGame m_myGame;
    private List<GameObject> m_ListDialog;

}
