﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public DelegMsgBox m_delegPopMsgBox;

	public MyGame(GameData gameData = null)
	{
		m_bEnd = false;
		m_ListMessageBox = new List<MessageBox>();
		m_msgGenerater = new Generater ();

		m_GameData = gameData;
		if (m_GameData == null) 
		{
			m_GameData = new GameData ();
			m_GameData.Init ();
		}

		//m_msgGenerater.Register(typeof(TestMessage));
		m_msgGenerater.Register (typeof(JsMsgBox));
	}

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        m_ListMessageBox.Clear();

		ArrayList lstMsgBox = m_msgGenerater.Generate ();

		foreach(object obj in lstMsgBox)
		{
			MessageBox testMsg =  obj as MessageBox;
			if (testMsg != null)
			{
				m_ListMessageBox.Add(testMsg);
			} 
		}
    }

	public GameData GetGameData()
	{
		return m_GameData;
	}

    private bool m_bEnd;
	private Generater m_msgGenerater;
	private GameData m_GameData;
    public List<MessageBox> m_ListMessageBox;
}

enum OFFICE_GROUP
{
	SanG,
	JiuQ
}

enum OFFICE
{
	ChengX,
	TaiW,
	YuSDF,
}

enum FACTION
{
    ShiDF,
    XunG,
    HuanG,
}

[Serializable]
public class GameData : ISerializationCallbackReceiver
{

	public GameData()
	{
		m_OfficeDict = new Tools.SerialDictionary<string, Office> ();
		m_FactionDict = new Dictionary<string, Faction> ();
		m_PersionDict = new Dictionary<string, Persion> ();
		m_officeResponse = new OfficeResponse ();
        m_factionReleation = new FactionReleation();
	}

	public void Init()
	{
		tm = 10;
		fk = 10;
		wb = 10;

		List<Office> sag = new List<Office> {new Office(OFFICE.ChengX.ToString()), new Office(OFFICE.TaiW.ToString()), new Office(OFFICE.YuSDF.ToString())};
		foreach(Office office in sag)
		{
			m_OfficeDict.Add (office.GetName(), office);
		}

        List<Faction> factionList = new List<Faction> {new Faction(FACTION.ShiDF.ToString()), new Faction(FACTION.HuanG.ToString()), new Faction(FACTION.XunG.ToString())};

		foreach(Faction faction in factionList)
		{
			m_FactionDict.Add (faction.GetName (), faction);
		}

		List<Persion> persionList = new List<Persion> (){new Persion("zh"), new Persion("wa"), new Persion("li")};

		foreach(Persion persion in persionList)
		{
			m_PersionDict.Add (persion.GetName(), persion);
		}

		InitOfficeResponse ();
        InitFactionReleation();
		
	}

	public void OnBeforeSerialize()
	{
		serialOffice = new List<Office> (m_OfficeDict.Values);
		serialFaction = new List<Faction> (m_FactionDict.Values);
		serialPersion = new List<Persion> (m_PersionDict.Values);
	}

	public void OnAfterDeserialize()
	{
		foreach(Office office in serialOffice)
		{
			m_OfficeDict.Add (office.GetName (), office);
		}

		foreach (Faction faction in serialFaction) 
		{
			m_FactionDict.Add (faction.GetName(), faction);
		}

		foreach (Persion persion in serialPersion)
		{
			m_PersionDict.Add (persion.GetName (), persion);
		}
	}


	private void InitOfficeResponse()
	{
		List<Persion> listPersion = new List<Persion> (m_PersionDict.Values);
		listPersion.Sort ((p1,p2)=> -(p1.m_score.CompareTo(p2.m_score)));

		m_officeResponse.Set (OFFICE.ChengX.ToString (), listPersion [0].GetName ());
		m_officeResponse.Set (OFFICE.TaiW.ToString (),   listPersion [1].GetName ());
		m_officeResponse.Set (OFFICE.YuSDF.ToString (),  listPersion [2].GetName ());
	}

    private void InitFactionReleation()
    {
        Persion persion = m_officeResponse.GetPersionByOffice(OFFICE.ChengX.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.ShiDF.ToString());

        persion = m_officeResponse.GetPersionByOffice(OFFICE.YuSDF.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.ShiDF.ToString());

        persion = m_officeResponse.GetPersionByOffice(OFFICE.TaiW.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.XunG.ToString());
    }

	public int tm;
	public int fk;
	public int wb;

	public Dictionary<string, Office>  m_OfficeDict;
	public Dictionary<string, Faction> m_FactionDict;
	public Dictionary<string, Persion> m_PersionDict;

	public OfficeResponse m_officeResponse;
    public FactionReleation m_factionReleation;

	[SerializeField]
	private List<Office> serialOffice;

	[SerializeField]
	private List<Faction> serialFaction;

	[SerializeField]
	private List<Persion> serialPersion;

}

[Serializable]
public class Office
{
	public Office(string name)
	{
		m_name = name;
	}

	public string GetName()
	{
		return m_name;
	}

	[SerializeField]
	public string m_name;
}

[Serializable]
public class Faction
{
	public Faction(string strIcon)
	{	
		m_name = strIcon;	
	}

	public string GetName()
	{
		return m_name;
	}

	[SerializeField]
	private string m_name;
}

[Serializable]
public class GameEnv
{
	public GameEnv()
	{
		m_lang = "CHI";
	}

	public String GetLang()
	{
		return m_lang;
	}

	public void SetLang(String lang)
	{
		m_lang = lang;
	}

	private string m_lang;
}

[Serializable]
public class Persion
{
	public Persion(String name)
	{
		m_name = name;
		m_score = ran.Next(1,100);
	}

	public string GetName()
	{
		return m_name;
	}

    public String GetScore()
    {
        return m_score.ToString();
    }

	public string m_name;
    public int m_score;
	private static System.Random ran=new System.Random();
}

[Serializable]
public class OfficeResponse
{
	public OfficeResponse()
	{
		m_list = new List<ELEMENT> ();
	}

	public void Set(String office, String persion)
	{
		Boolean bfind = false;
		for(int i=0; i<m_list.Count; i++)
		{
			ELEMENT elem = m_list [i];
			if (elem.persion == persion)
			{
				elem.persion = null;
			}

			if (elem.office == office) 
			{
				elem.persion = persion;
				bfind = true;
			}
		}
		if(!bfind)
		{
			m_list.Add (new ELEMENT(office, persion));
		}
	}

	public Persion GetPersionByOffice(String office)
	{
		for(int i=0; i<m_list.Count; i++)
		{
			if (m_list[i].office == office)
			{
                String str = m_list[i].persion;

                Persion persion = Global.GetGameData().m_PersionDict[str];
                return persion;
			}
		}

		return null;
	}

	public Office GetOfficeByPersion(String persion)
	{
		for(int i=0; i<m_list.Count; i++)
		{
			if (m_list[i].persion == persion)
			{
				return Global.GetGameData().m_OfficeDict[m_list[i].office];
			}
		}

		return null;
	}

	[Serializable]
	struct ELEMENT
	{
		public ELEMENT(String office, String persion)
		{
			this.office = office;
			this.persion = persion;
		}

		[SerializeField]
		public String office;

		[SerializeField]
		public String persion;
	}

	[SerializeField]
	private List<ELEMENT> m_list;

}

[Serializable]
public class FactionReleation
{
    public FactionReleation()
    {
        m_list = new List<ELEMENT> ();
    }

    public void Set(String persionName, String factionName)
    {
        for(int i=0; i<m_list.Count; i++)
        {
            ELEMENT elem = m_list [i];
            if (elem.fationName == factionName)
            {
                elem.persionList.Add(persionName);
                return;
            }
        }

        ELEMENT elem2 = new ELEMENT(factionName);
        elem2.persionList.Add(persionName);
        m_list.Add (elem2);
    }

    public List<Persion> GetPersionByOffice(String factionName)
    {
        List<Persion> persionList = new List<Persion>();
        for(int i=0; i<m_list.Count; i++)
        {
            if (m_list[i].fationName == factionName)
            {
                foreach (string persionName in m_list[i].persionList)
                {
                    persionList.Add(Global.GetGameData().m_PersionDict[persionName]);
                }
                break;
            }
        }

        return persionList;
    }

    public Faction GetFactionByPersion(String persion)
    {
        for(int i=0; i<m_list.Count; i++)
        {
            if (m_list[i].persionList.Contains(persion))
            {
                return Global.GetGameData().m_FactionDict[m_list[i].fationName];
            }
        }

        return null;
    }

    [Serializable]
    struct ELEMENT
    {
        public ELEMENT(String name)
        {
            fationName = name;
            persionList = new List<string>();
        }

        [SerializeField]
        public String fationName;

        [SerializeField]
        public List<String> persionList;
    }

    [SerializeField]
    private List<ELEMENT> m_list;

}