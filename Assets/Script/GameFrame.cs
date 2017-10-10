using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFrame : MonoBehaviour
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
		string strSavePath = Application.persistentDataPath + "//save";
		Debug.Log(strSavePath);
		if (!Directory.Exists(strSavePath))
		{
			Directory.CreateDirectory(strSavePath);
		}

		string json = JsonUtility.ToJson (Global.GetMyGame().GetGameData ());
		File.WriteAllText (Application.persistentDataPath + "\\save\\game.save", json);
	}

	public void OnLoad()
	{
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

	private static GameFrame m_Instance;
}

