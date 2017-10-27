using System;
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

[Serializable]
public class GameData : ISerializationCallbackReceiver
{

	public GameData()
	{
		m_OfficeDict = new Tools.SerialDictionary<string, Office> ();
		m_FactionDict = new Dictionary<string, Faction> ();
		m_PersionDict = new Dictionary<string, Persion> ();
	}

	public void Init()
	{
		tm = 10;
		fk = 10;
		wb = 10;



		List<Office> sag = new List<Office> {new Office("ChengX"), new Office("TaiW"), new Office("YuSDF")};
		foreach(Office office in sag)
		{
			m_OfficeDict.Add (office.GetName(), office);
		}

		List<Faction> factionList = new List<Faction> {new Faction("sid"), new Faction("hug"), new Faction("xug")};

		foreach(Faction faction in factionList)
		{
			m_FactionDict.Add (faction.GetName (), faction);
		}

		List<Persion> persionList = new List<Persion> (){new Persion("zh"), new Persion("wa"), new Persion("li")};

		foreach(Persion persion in persionList)
		{
			m_PersionDict.Add (persion.GetName(), persion);
		}
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


	public int tm;
	public int fk;
	public int wb;


	public Dictionary<string, Office>  m_OfficeDict;
	public Dictionary<string, Faction> m_FactionDict;
	public Dictionary<string, Persion> m_PersionDict;

	public Releation m_Releation;

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
	}

	public string GetName()
	{
		return m_name;
	}

	public string m_name;
	public int m_score;
}

[Serializable]
public class Releation
{
	public Releation()
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

	public String GetPersionByOffice(String office)
	{
		for(int i=0; i<m_list.Count; i++)
		{
			if (m_list[i].office == office)
			{
				return m_list[i].persion;
			}
		}

		return null;
	}

	public String GetOfficeByPersion(String persion)
	{
		for(int i=0; i<m_list.Count; i++)
		{
			if (m_list[i].persion == persion)
			{
				return m_list[i].office;
			}
		}

		return null;
	}

	struct ELEMENT
	{
		public ELEMENT(String office, String persion)
		{
			this.office = office;
			this.persion = persion;
		}

		public String office;
		public String persion;
	}

	List<ELEMENT> m_list;

}