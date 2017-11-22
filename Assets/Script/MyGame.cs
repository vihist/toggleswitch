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

        m_GameData.m_Date.Increase();
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

    TaiC,
    TaiP,
    WeiW,
    GuangL,
    TingW,
    DaH,
    ZhongZ,
    DaS,
    ShaoF,

	Youzhou=ZHOUMING.Youzhou,
	Jizhou=ZHOUMING.Jizhou,
	Bingzhou=ZHOUMING.Bingzhou,
	Yuzhou=ZHOUMING.Yuzhou,
	Yanzhou=ZHOUMING.Yanzhou,
	Qingzhou=ZHOUMING.Qingzhou,
	Xuzhou=ZHOUMING.Xuzhou,
	Yangzhou=ZHOUMING.Yangzhou,
	Yongzhou=ZHOUMING.Yongzhou,
	Liangzhou=ZHOUMING.Liangzhou,
	Jingzhou=ZHOUMING.Jingzhou,
	Sizhou=ZHOUMING.Sizhou,
	Yizhou=ZHOUMING.Yizhou
}

enum FEIPIN
{
    HuangH,
    GuiF1,
    GuiF2,
    GuiF3,
}

enum FACTION
{
    ShiDF,
    XunG,
    HuanG,
}

enum ZHOUMING
{
    Youzhou=255,
    Jizhou,
    Bingzhou,
    Yuzhou,
    Yanzhou,
    Qingzhou,
    Xuzhou,
    Yangzhou,
    Yongzhou,
    Liangzhou,
    Jingzhou,
    Sizhou,
    Yizhou
}

[Serializable]
public class GameData : ISerializationCallbackReceiver
{

	public GameData()
	{
        m_Date = new GameDate();
        m_OfficeDict = new Tools.SerialDictionary<string, Office> ();
        m_HougongOfficeDict = new Dictionary<string, Office>();
        m_FactionDict = new Dictionary<string, Faction> ();

		m_MaleDict = new Dictionary<string, Persion> ();
        m_FemaleDict = new Dictionary<string, Persion>();

        m_officeResponse = new OfficeResponse ();
        m_factionReleation = new FactionReleation();
        m_HougongOfficeResponse = new OfficeResponse();
    }

	public void Init()
	{
		tm = 10;
		fk = 10;
		wb = 10;

        foreach (OFFICE eOffice in Enum.GetValues(typeof(OFFICE)))
        {
            Office office = new Office(eOffice.ToString());
            m_OfficeDict.Add(office.GetName(), office);
        }

        foreach (FACTION eFaction in Enum.GetValues(typeof(FACTION)))
        {
            Faction faction = new Faction(eFaction.ToString());
            m_FactionDict.Add(faction.GetName(), faction);
        }

        for(int i=0; i< m_OfficeDict.Count; i++)
        {
            Persion persion = new Persion(Persion.GetMaleName());
			if (m_MaleDict.ContainsKey (persion.GetName ())) 
			{
				i--;
				continue;
			}

            m_MaleDict.Add(persion.GetName(), persion);
        }

		List<int> listScore = Tools.Probability.GetRandomNumArrayWithStableSum (Enum.GetNames(typeof(FEIPIN)).Length, 100);

        for (int i = 0; i < Enum.GetValues(typeof(FEIPIN)).Length; i++)
        {
			Persion persion = new Persion(Persion.GetFemaleName());
			if (m_FemaleDict.ContainsKey(persion.GetName()))
            {
                i--;
                continue;
            }
			persion.m_score = listScore [i];
			m_FemaleDict.Add(persion.GetName(), persion);
        }



        InitOfficeResponse ();
		InitHougongOfficeResponse ();

        InitFactionReleation();
		
	}

	public void OnBeforeSerialize()
	{
		serialOffice = new List<Office> (m_OfficeDict.Values);
		serialHougongOffice = new List<Office> (m_HougongOfficeDict.Values);

		serialFaction = new List<Faction> (m_FactionDict.Values);
		serialMale = new List<Persion> (m_MaleDict.Values);
		serialFemale = new List<Persion> (m_FemaleDict.Values);

	}

	public void OnAfterDeserialize()
	{
		foreach(Office office in serialOffice)
		{
			m_OfficeDict.Add (office.GetName (), office);
		}

		foreach(Office office in serialHougongOffice)
		{
			m_HougongOfficeDict.Add (office.GetName (), office);
		}

		foreach (Faction faction in serialFaction) 
		{
			m_FactionDict.Add (faction.GetName(), faction);
		}

		foreach (Persion persion in serialMale)
		{
			m_MaleDict.Add (persion.GetName (), persion);
		}

		foreach (Persion persion in serialFemale)
		{
			m_FemaleDict.Add (persion.GetName (), persion);
		}


	}


	private void InitOfficeResponse()
	{
		List<Persion> listPersion = new List<Persion> (m_MaleDict.Values);
		listPersion.Sort ((p1,p2)=> -(p1.m_score.CompareTo(p2.m_score)));

		int i = 0;
		foreach (OFFICE eOffice in Enum.GetValues(typeof(OFFICE)))
        {
            m_officeResponse.Set(eOffice.ToString(), listPersion[i].GetName());
			i++;
        }
	}

	private void InitHougongOfficeResponse()
	{
		List<Persion> listPersion = new List<Persion> (m_FemaleDict.Values);

		int i = 0;
		foreach (FEIPIN eOffice in Enum.GetValues(typeof(FEIPIN)))
		{
			m_HougongOfficeResponse.Set(eOffice.ToString(), listPersion[i].GetName());
			i++;
		}
	}

    private void InitFactionReleation()
    {
        Persion persion = m_officeResponse.GetPersionByOffice(OFFICE.ChengX.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.ShiDF.ToString());

        persion = m_officeResponse.GetPersionByOffice(OFFICE.YuSDF.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.ShiDF.ToString());

        persion = m_officeResponse.GetPersionByOffice(OFFICE.TaiW.ToString());
        m_factionReleation.Set(persion.GetName(), FACTION.XunG.ToString());

        foreach(String name in m_MaleDict.Keys)
        {
            if(m_factionReleation.GetFactionByPersion(name) == null)
            {
                FACTION eFaction = (FACTION)(Tools.Probability.GetRandomNum(0, 2));
                m_factionReleation.Set(name, eFaction.ToString());
            }
        }

		foreach(String name in m_FemaleDict.Keys)
		{
			if(m_factionReleation.GetFactionByPersion(name) == null)
			{
				FACTION eFaction = (FACTION)(Tools.Probability.GetRandomNum(0, 2));
				m_factionReleation.Set(name, eFaction.ToString());
			}
		}
    }

	public int tm;
	public int fk;
	public int wb;
    public GameDate m_Date;

    public Dictionary<string, Office>  m_OfficeDict;
    public Dictionary<string, Office>  m_HougongOfficeDict;
    public Dictionary<string, Faction> m_FactionDict;
	public Dictionary<string, Persion> m_MaleDict;
    public Dictionary<string, Persion> m_FemaleDict;

    public OfficeResponse m_officeResponse;
    public OfficeResponse m_HougongOfficeResponse;
    public FactionReleation m_factionReleation;

	[SerializeField]
	private List<Office> serialOffice;

	[SerializeField]
	private List<Office> serialHougongOffice;

	[SerializeField]
	private List<Faction> serialFaction;

	[SerializeField]
	private List<Persion> serialMale;

	[SerializeField]
	private List<Persion> serialFemale;

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

    public static String GetMaleName()
    {
        int rowCount = Tools.Probability.GetRandomNum(1, cvsXings.RowLength() - 1);
        String xingshi = cvsXings.Get(rowCount.ToString(), "CHI");

        rowCount = Tools.Probability.GetRandomNum(1, cvsMingz.RowLength() - 1);
        String mingzi = cvsMingz.Get(rowCount.ToString(), "CHI");

        return xingshi + mingzi;
    }

    public static String GetFemaleName()
    {
        int rowCount = Tools.Probability.GetRandomNum(1, cvsXings.RowLength() - 1);
        String xingshi = cvsXings.Get(rowCount.ToString(), "CHI");

        String mingzi = cvsMingz.Get("0", "CHI");

        return xingshi + mingzi;
    }


    public string m_name;
    public int m_score;
	private static System.Random ran=new System.Random();

    private static Tools.Cvs cvsXings = new Tools.Cvs("text/xingshi");
    private static Tools.Cvs cvsMingz = new Tools.Cvs("text/mingzi");
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

				if (Global.GetGameData ().m_MaleDict.ContainsKey (str)) 
				{
					return Global.GetGameData ().m_MaleDict [str];
				}
                
				if (Global.GetGameData ().m_FemaleDict.ContainsKey (str)) 
				{
					return Global.GetGameData ().m_FemaleDict [str];
				}
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
				String str = m_list [i].office;

				if (Global.GetGameData ().m_OfficeDict.ContainsKey (str)) 
				{
					return Global.GetGameData ().m_OfficeDict [str];
				}

				if (Global.GetGameData ().m_HougongOfficeDict.ContainsKey (str)) 
				{
					return Global.GetGameData ().m_HougongOfficeDict [str];
				}
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
                    persionList.Add(Global.GetGameData().m_MaleDict[persionName]);
                }
                break;
            }
        }

        return persionList;
    }

    public Faction GetFactionByPersion(String persion)
    {
        try
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i].persionList.Contains(persion))
                {
                    return Global.GetGameData().m_FactionDict[m_list[i].fationName];
                }
            }
        }
        catch (Exception)
        {

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

[Serializable]
public class GameDate
{
    public GameDate()
    {
        _year = 1;
        _month = 1;
        _day = 1;
    }

    public void Increase()
    {
        if (_day == 30)
        {
            if (_month == 12)
            {
                _year++;
                _month = 1;
            }
            else
            {
                _month++;
            }
            _day = 1;
        }
        else
        {
            _day++;
        }
    }

    public int year
    {
        get
        {
            return _year;
        }
    }

    public int month
    {
        get
        {
            return _month;
        }
    }

    public int day
    {
        get
        {
            return _day;
        }
    }

    public override string ToString()
    {
        return _year.ToString() + "年" + _month + "月" + _day + "日";
    }

    public bool Is(string str)
    {
        string[] arr = str.Split('/');
        if (arr.Length < 3)
        {
            throw new Exception();
        }

        if (arr[0] != "*")
        {
            if (Convert.ToInt16(arr[0]) != _year)
            {
                return false;
            }
        }

        if (arr[1] != "*")
        {
            if (Convert.ToInt16(arr[1]) != _month)
            {
                return false;
            }
        }

        if (arr[2] != "*")
        {
            if (Convert.ToInt16(arr[2]) != _day)
            {
                return false;
            }
        }

        return true;
    }

    [SerializeField]
    private int _year;
    [SerializeField]
    private int _month;
    [SerializeField]
    private int _day;
}