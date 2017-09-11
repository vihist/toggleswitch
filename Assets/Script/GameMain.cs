
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        Invoke("OnTimer", 1.0F);  //2秒后，没0.3f调用一次
    }

    private void OnTimer()
    {
        try
        {
            Debug.Log("OnTimer");

            m_myGame.NextTurn();

            GameObject UIRoot = GameObject.Find("Canvas");

            GameObject dialog = Instantiate(Resources.Load("EasyMenu/_Prefabs/Dialog"), UIRoot.transform) as GameObject;

            Text txTitle = dialog.transform.Find("Title").GetComponent<Text>();
            txTitle.text = "Title";

            Text txContent = dialog.transform.Find("Content").GetComponent<Text>();
            txContent.text = "Contentsdfgdsfgsdfgssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

            GameObject ButtonObj = Instantiate(Resources.Load("EasyMenu/_Prefabs/Button"), dialog.transform) as GameObject;
            ButtonObj.transform.position = txContent.transform.position - new Vector3(10,10,0);
            //GameObject ButtonObj1 = Instantiate(Resources.Load("EasyMenu/_Prefabs/Button"), dialog.transform) as GameObject;
            Button Btn = ButtonObj.GetComponent<Button>();
            Btn.onClick.AddListener
                (
                delegate ()
                {
                    this.OnClick(ButtonObj);
                }
                );

            //Invoke("OnTimer", 1.0F);
        }
        catch (EndGameException )
        {
            SceneManager.LoadScene("EndScene");
            return;
        }

    }

    public void OnClick(GameObject sender)
    {
        Debug.Log("OnClick" + sender);

    }

    private MyGame m_myGame;
}
