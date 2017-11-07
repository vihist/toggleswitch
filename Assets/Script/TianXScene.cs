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
	void Update () 
	{
		for(int i=(int)OFFICE.Youzhou; i<(int)OFFICE.Yizhou+1; i++)
		{
			
			RefreshOfficeResposne ((OFFICE)i);
		}
	}

	private void RefreshOfficeResposne(OFFICE enZhouming)
	{
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice(enZhouming.ToString());

		if (persion != null) 
		{
			GameObject UIRoot = GameObject.Find("Map");
			Transform officeGameObj = UIRoot.transform.Find (enZhouming.ToString ());

			Transform persionObj = officeGameObj.Find ("Persion").transform;
			persionObj.Find ("Text").GetComponent<Text> ().text = persion.GetName ();

			officeGameObj.Find ("Persion").transform.Find ("score").transform.Find("Text").GetComponent<Text> ().text = persion.GetScore ();
			officeGameObj.Find ("Persion").transform.Find ("faction").transform.Find("Text").GetComponent<Text>().text 
			= UIFrame.GetUiDesc(Global.GetGameData().m_factionReleation.GetFactionByPersion(persion.GetName()).GetName());
		}
	}
}
