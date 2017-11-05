using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TianXScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject UIRoot = GameObject.Find("Map");

        foreach (ZHOUMING Zhouming in Enum.GetValues(typeof(ZHOUMING)))
        {
            UIRoot.transform.Find(Zhouming.ToString()).transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(Zhouming.ToString());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
