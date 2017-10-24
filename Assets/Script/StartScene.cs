using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScene : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

/*		string json = File.ReadAllText (Application.persistentDataPath + "/game.env");
		if (json == null) 
		{
			Global.SetEnv (new GameEnv());
		}
		else
		{
			Global.SetEnv (JsonUtility.FromJson<GameEnv>(json));
		}
*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnButtonNew()
	{
		GameFrame.GetInstance ().OnNew ();
	}

	public void OnButtonQuit()
	{
		GameFrame.GetInstance().OnQuit();
	}

	public void OnButtonLoad()
	{
		GameFrame.GetInstance ().OnLoad ();
	}
}
