using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaoTScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		GameObject sanGong= GameObject.Find(OFFICE_GROUP.SanG.ToString());
		sanGong.transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(OFFICE_GROUP.SanG.ToString());

		sanGong.transform.Find(OFFICE.ChengX.ToString()).transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(OFFICE.ChengX.ToString());
		sanGong.transform.Find(OFFICE.TaiW.ToString()).transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(OFFICE.TaiW.ToString());
		sanGong.transform.Find(OFFICE.YuSDF.ToString()).transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc(OFFICE.YuSDF.ToString());

        GameObject jiuQing = GameObject.Find("JiuQ");
        jiuQing.transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("JiuQ");
        jiuQing.transform.Find("TaiC").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TaiC");
        jiuQing.transform.Find("TaiP").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TaiP");
        jiuQing.transform.Find("WeiW").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("WeiW");
        jiuQing.transform.Find("GuangL").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("GuangL");
        jiuQing.transform.Find("TingW").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("TingW");
        jiuQing.transform.Find("DaH").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("DaH");
        jiuQing.transform.Find("ZhongZ").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("ZhongZ");
        jiuQing.transform.Find("DaS").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("DaS");
        jiuQing.transform.Find("ShaoF").transform.Find("Text").GetComponent<Text>().text = UIFrame.GetUiDesc("ShaoF");
    }
	
	// Update is called once per frame
	void Update ()
    {
		RefreshOfficeResposne (OFFICE_GROUP.SanG, OFFICE.ChengX);
		RefreshOfficeResposne (OFFICE_GROUP.SanG, OFFICE.TaiW);
		RefreshOfficeResposne (OFFICE_GROUP.SanG, OFFICE.YuSDF);
	}

	private void RefreshOfficeResposne(OFFICE_GROUP enofficeGroup, OFFICE enOffice)
	{
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice(enOffice.ToString());

		if (persion != null) 
		{
			Transform officeChengx = GameObject.Find (enofficeGroup.ToString ()).transform.Find (enOffice.ToString ());
			officeChengx.Find ("Persion").transform.Find ("Text").GetComponent<Text> ().text = persion.GetName ();
            officeChengx.Find ("Persion").transform.Find ("Score").transform.Find("Text").GetComponent<Text> ().text = persion.GetScore ();

            officeChengx.Find("Persion").transform.Find("Faction").transform.Find("Text").GetComponent<Text>().text 
                = UIFrame.GetUiDesc(Global.GetGameData().m_factionReleation.GetFactionByPersion(persion.GetName()).GetName());
        }
	}
}
