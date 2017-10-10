using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
