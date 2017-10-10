using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFrame
{
	public static GameFrame GetInstance()
	{
		if (m_Instance == null) 
		{
			m_Instance = new GameFrame ();
		}

		return m_Instance;
	}

	public void OnNew()
	{
		Global.SetMyGame (new MyGame ());
		SceneManager.LoadSceneAsync("MainScene");
	}

	public void OnSave()
	{
		string strSavePath = GetSavePath ();
		Debug.Log(strSavePath);
		if (!Directory.Exists(strSavePath))
		{
			Directory.CreateDirectory(strSavePath);
		}

		string json = JsonUtility.ToJson (Global.GetMyGame().GetGameData ());
		File.WriteAllText (GetSavePath() + "/game.save", json);
	}

	public void OnLoad()
	{
		string strSavePath = GetSavePath();
		Debug.Log(strSavePath);

		string json = File.ReadAllText (GetSavePath() + "/game.save");
		Global.SetMyGame (JsonUtility.FromJson<MyGame>(json));
		SceneManager.LoadSceneAsync("MainScene");
	}

	public void OnEnd()
	{
		SceneManager.LoadSceneAsync ("EndScene");
	}

	public void OnQuit()
	{
		Application.Quit ();
	}

	public void OnReturn()
	{
		SceneManager.LoadSceneAsync ("StartScene");
	}

	private string GetSavePath()
	{
		return Application.persistentDataPath + "/save";
	}

	private static GameFrame m_Instance;
}

