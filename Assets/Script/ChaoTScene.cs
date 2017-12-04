using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class ChaoTScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        GameObject sanGong= GameObject.Find(OFFICE_GROUP.SanG.ToString());
		sanGong.transform.Find("Text").GetComponent<Text>().text = Cvs.UiDesc.Get(OFFICE_GROUP.SanG.ToString());

        for (int i=0; i<3; i++)
        {
            OFFICE eOffice = (OFFICE)i;
            sanGong.transform.Find(eOffice.ToString()).transform.Find("Text").GetComponent<Text>().text = (eOffice.ToString());

        }

        GameObject jiuQing = GameObject.Find(OFFICE_GROUP.JiuQ.ToString());
        jiuQing.transform.Find("Text").GetComponent<Text>().text = Cvs.UiDesc.Get(OFFICE_GROUP.JiuQ.ToString());

        for (int i = 3; i < 3+9; i++)
        {
            OFFICE eOffice = (OFFICE)i;
            jiuQing.transform.Find(eOffice.ToString()).transform.Find("Text").GetComponent<Text>().text = Cvs.UiDesc.Get(eOffice.ToString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            OFFICE eOffice = (OFFICE)i;
            RefreshOfficeResposne(OFFICE_GROUP.SanG, eOffice);
        }

        for (int i = 3; i < 3+9; i++)
        {
            OFFICE eOffice = (OFFICE)i;
            RefreshOfficeResposne(OFFICE_GROUP.JiuQ, eOffice);
        }
    }

	private void RefreshOfficeResposne(OFFICE_GROUP enofficeGroup, OFFICE enOffice)
	{
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice(enOffice.ToString());

		if (persion != null) {
			Transform officeGameObj = GameObject.Find (enofficeGroup.ToString ()).transform.Find (enOffice.ToString ());
			officeGameObj.Find ("Persion").transform.Find ("Text").GetComponent<Text> ().text = persion.GetName ();

			officeGameObj.Find ("Persion").transform.Find ("Score").transform.Find ("Text").GetComponent<Text> ().text = persion.GetScore ();
			officeGameObj.Find ("Persion").transform.Find ("Faction").transform.Find ("Text").GetComponent<Text> ().text 
                = Cvs.UiDesc.Get (Global.GetGameData ().m_factionReleation.GetFactionByPersion (persion.GetName ()).GetName ());
		} 
		else
		{
			Transform officeGameObj = GameObject.Find (enofficeGroup.ToString ()).transform.Find (enOffice.ToString ());
			officeGameObj.Find ("Persion").transform.Find ("Text").GetComponent<Text> ().text = "--";

			officeGameObj.Find ("Persion").transform.Find ("Score").transform.Find ("Text").GetComponent<Text> ().text = "0";
			officeGameObj.Find ("Persion").transform.Find ("Faction").transform.Find ("Text").GetComponent<Text> ().text 
			= "-";
		}
	}
}
