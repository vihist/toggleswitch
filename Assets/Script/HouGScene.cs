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
	void Update ()
	{
		foreach (FEIPIN feiPin in Enum.GetValues(typeof(FEIPIN)))
		{
			RefreshOfficeResposne(feiPin);
		}
	}

	void RefreshOfficeResposne(FEIPIN eFeiPin)
	{
		Persion persion = Global.GetGameData ().m_HougongOfficeResponse.GetPersionByOffice(eFeiPin.ToString());

		if (persion != null) 
		{
			Transform officeGameObj = GameObject.Find (eFeiPin.ToString ()).transform;
			officeGameObj.Find ("Persion").transform.Find ("Text").GetComponent<Text> ().text = persion.GetName ();

			officeGameObj.Find ("Persion").transform.Find ("Score").transform.Find("Text").GetComponent<Text> ().text = persion.GetScore ();
			officeGameObj.Find ("Persion").transform.Find ("Faction").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(Global.GetGameData().m_factionReleation.GetFactionByPersion(persion.GetName()).GetName());
		}
	}
}
