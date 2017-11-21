using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouGScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        foreach (FEIPIN feiPin in Enum.GetValues(typeof(FEIPIN)))
        {
            GameObject.Find(feiPin.ToString()).transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(feiPin.ToString());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
