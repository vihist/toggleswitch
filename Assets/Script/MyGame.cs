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
public class GameData
{

	public GameData()
	{
		tm = 10;
		fk = 10;
		wb = 10;

		sag = new List<Office> {new Office("chx"), new Office("tiw"), new Office("ysdf")};
		factionList = new List<Faction> {new Faction("sid"), new Faction("hug"), new Faction("xug")};

		m_OfficeDict = new Dictionary<string, Office> ();
		foreach(Office office in sag)
		{
			m_OfficeDict.Add (office.GetName(), office);
		}

		m_FactionDict = new Dictionary<string, Faction> ();
		foreach(Faction faction in factionList)
		{
			m_FactionDict.Add (faction.GetName (), faction);
		}
	}

	public void Init()
	{
		m_OfficeDict["chx"].m_faction = "sid";
	}

	public int tm;
	public int fk;
	public int wb;
	public List<Office> sag;
	public List<Office> juq;
	public List<Faction> factionList;
	public Dictionary<string, Office> m_OfficeDict;
	public Dictionary<string, Faction> m_FactionDict;
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

	[SerializeField]
	public string m_faction;
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