using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public DelegMsgBox m_delegPopMsgBox;

	public MyGame(GameData gameData)
	{
		m_GameData = gameData;
		InitGame ();
    }

	public MyGame(String countryName, String yearName, String familyName, String selfName)
	{
		m_GameData = new GameData (countryName, yearName, familyName, selfName);
		InitGame ();
	}

	public void InitGame()
	{
		m_bEnd = false;
		m_ListMessageBox = new List<MessageBox>();
		m_msgGenerater = new Generater ();

		//m_msgGenerater.Register(typeof(TestMessage));
		m_msgGenerater.Register(typeof(CT_SanggongKongQue));
		m_msgGenerater.Register(typeof(JsMsgBox));
		m_msgGenerater.Register(typeof(TX_yinghuoshouxin));
		m_msgGenerater.Register(typeof(TestMessage2));
        m_msgGenerater.Register(typeof(CT_JiuqingKongQue));
        m_msgGenerater.Register(typeof(DF_CishiKongQue));
    }

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        m_ListMessageBox.Clear();

        MessageBox testMsg = m_msgGenerater.GenerateEvent() as MessageBox;
        if (testMsg != null)
        {
            m_ListMessageBox.Add(testMsg);
            return;
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

public enum OFFICE_GROUP
{
	SanGong,
	JiuQing,
    CiShi,
}

public enum OFFICE
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
	//Yanzhou=ZHOUMING.Yanzhou,
	Qingzhou=ZHOUMING.Qingzhou,
	Xuzhou=ZHOUMING.Xuzhou,
	Yangzhou=ZHOUMING.Yangzhou,
	Yongzhou=ZHOUMING.Yongzhou,
	//Liangzhou=ZHOUMING.Liangzhou,
	Jingzhou=ZHOUMING.Jingzhou,
	//Sizhou=ZHOUMING.Sizhou,
	Yizhou=ZHOUMING.Yizhou,
}

enum FEIPIN
{
    HuangH,
    GuiF1,
    GuiF2,
    GuiF3,
    Feib1,
    Feib2,
    Feib3,
    Feib4,
    Feib5,
    Feib6,
    Feib7,
    Feib8,
    Feib9,
}

public enum FACTION
{
    ShiDF,
    XunG,
    //HuanG,
	WaiQ,
}

enum ZHOUMING
{
    Youzhou=255,
    Jizhou,
    Bingzhou,
    Yuzhou,
    //Yanzhou,
    Qingzhou,
    Xuzhou,
    Yangzhou,
    Yongzhou,
    //Liangzhou,
    Jingzhou,
    //Sizhou,
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

		m_Emperor = new Emperor ();

	}
	public GameData(String countryName, String yearName, String familyName, String selfName):this()
	{
		m_Emperor.Init(familyName, selfName);
		m_CountryName = countryName;
		m_YearName = yearName;
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

    public List<OFFICE> GetOfficeEnum(OFFICE_GROUP eGroup)
    {
        switch (eGroup)
        {
            case OFFICE_GROUP.SanGong:
                {
                    List<OFFICE> offList = new List<OFFICE> { OFFICE.ChengX, OFFICE.TaiW, OFFICE.YuSDF };
                    return offList;
                }
                break;
            case OFFICE_GROUP.JiuQing:
                {
                    List<OFFICE> offList = new List<OFFICE> { OFFICE.TaiC, OFFICE.TaiP, OFFICE.GuangL, OFFICE.TingW, OFFICE.DaH, OFFICE.ZhongZ, OFFICE.DaS, OFFICE.ShaoF };
                    return offList;
                }
                break;
            case OFFICE_GROUP.CiShi:
                {
                    List<OFFICE> offList = new List<OFFICE> { OFFICE.Youzhou, OFFICE.Jizhou, OFFICE.Bingzhou, OFFICE.Yuzhou, OFFICE.Qingzhou, OFFICE.Xuzhou, OFFICE.Yangzhou, OFFICE.Yongzhou, OFFICE.Jingzhou, OFFICE.Yizhou };
                    return offList;
                }
                break;
            default:
                return null;
        }

    }

    public List<Persion> GetPersionByOfficeGroup(OFFICE_GROUP eGroup)
	{
        List<OFFICE> offList = GetOfficeEnum(eGroup);

        List<Persion> persionList = new List<Persion> ();
		foreach (OFFICE eOffice in offList)
		{
			Persion persion = m_officeResponse.GetPersionByOffice (eOffice.ToString ());
			if (persion != null) 
			{
				persionList.Add (persion);
			}
		}

		return persionList;
	}

	public Persion GetPersionByOffice(OFFICE eOffice)
	{
		return m_officeResponse.GetPersionByOffice (eOffice.ToString ());
	}

	public Faction GetFaction(FACTION eFaction)
	{
		return m_FactionDict [eFaction.ToString ()];
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

//		foreach(String name in m_FemaleDict.Keys)
//		{
//			if(m_factionReleation.GetFactionByPersion(name) == null)
//			{
//				FACTION eFaction = (FACTION)(Tools.Probability.GetRandomNum(0, 2));
//				m_factionReleation.Set(name, eFaction.ToString());
//			}
//		}
    }
	public String m_CountryName;
	public String m_YearName;

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
    public Emperor m_Emperor;

	public Persion m_persionWaitWQ;

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
public class Emperor
{
	public int age;
    public int heath;
    public int despress;
	public String familyName;
	public String selfName;

	public void Init(String familyName, String selfName)
	{
		this.familyName = familyName;
		this.selfName = selfName;
	}

	public String GetName()
	{
		return familyName + selfName;
	}

	public Emperor()
    {
		age = Tools.Probability.GetGaussianRandomNum (16, 40);
        heath = Tools.Probability.GetRandomNum(1,10);
        despress = Tools.Probability.GetRandomNum(1,10);
    }
}

[Serializable]
public class Office
{
	public Office(String office)
	{
		this.office = office;
	}

	public string GetName()
	{
		return office;
	}

	public int GetPower()
	{
		OFFICE eOffice = GetEnum ();

        switch (eOffice)
		{
		case OFFICE.ChengX:
			return 10;
		case OFFICE.TaiW:
			return 8;
		case OFFICE.YuSDF:
			return 7;
		case OFFICE.TaiC:
		case OFFICE.TaiP:
		case OFFICE.WeiW:
		case OFFICE.GuangL:
		case OFFICE.TingW:
		case OFFICE.DaH:
		case OFFICE.ZhongZ:
		case OFFICE.DaS:
		case OFFICE.ShaoF:
			return 5;
        case OFFICE.Youzhou:
	    case OFFICE.Jizhou:
        case OFFICE.Bingzhou:
        case OFFICE.Yuzhou:
        //case OFFICE.Yanzhou:
        case OFFICE.Qingzhou:
        case OFFICE.Xuzhou:
        case OFFICE.Yangzhou:
        case OFFICE.Yongzhou:
        //case OFFICE.Liangzhou:
        case OFFICE.Jingzhou:
        //case OFFICE.Sizhou:
        case OFFICE.Yizhou:
            return 3;
        default:
            return -1;
        }
	}

    public OFFICE GetEnum()
	{
		return (OFFICE)Enum.Parse(typeof(OFFICE), office);
	}

	[SerializeField]
	public String office;
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

	public string GetFullName()
	{
		return m_name+"_FULL";
	}

	public FACTION GetEnum()
	{
		return (FACTION)Enum.Parse(typeof(FACTION), m_name);
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
	public Persion(String name, int score = -1)
	{
		m_name = name;
		m_score = score;

		if (m_score == -1)
		{
			m_score = ran.Next(1,100);
		}
		
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
		return GetFamilyName() + GetSelfName();
    }

    public static String GetFemaleName()
    {
		return GetFamilyName() + Tools.Cvs.Mingz.Get("0", "CHI");
    }

	public static String GetFamilyName()
	{
		int rowCount = Tools.Probability.GetRandomNum(1, Tools.Cvs.Xings.RowLength() - 1);
		return Tools.Cvs.Xings.Get(rowCount.ToString(), "CHI");
	}

	public static String GetSelfName()
	{
		int rowCount = Tools.Probability.GetRandomNum(1, Tools.Cvs.Mingz.RowLength() - 1);
		return Tools.Cvs.Mingz.Get(rowCount.ToString(), "CHI");
	}

	public void Die()
	{
		Office office = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (GetName());

		Global.GetGameData ().m_officeResponse.Set (office.GetName(), "");
		Global.GetGameData ().m_factionReleation.RemovePersion (m_name);

		Global.GetGameData ().m_MaleDict.Remove (m_name);
	}

	public  void Quit()
	{
		Office office = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (GetName());

		Global.GetGameData ().m_officeResponse.Set (office.GetName(), "");
		Global.GetGameData ().m_factionReleation.RemovePersion (m_name);

		Global.GetGameData ().m_MaleDict.Remove (m_name);

	}

	public Office GetOffice()
	{
		Office office = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (GetName());
		return office;
	}

	public Faction GetFaction()
	{
		Faction faction = Global.GetGameData ().m_factionReleation.GetFactionByPersion (GetName());
		return faction;
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
				elem.persion = "";

				m_list [i] = elem;
			}

			if (elem.office == office) 
			{
				elem.persion = persion;
				m_list [i] = elem;

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

				m_list [i] = elem;
                return;
            }
        }

        ELEMENT elem2 = new ELEMENT(factionName);
        elem2.persionList.Add(persionName);
        m_list.Add (elem2);
    }

	public void RemovePersion(String persionName)
	{
		for(int i=0; i<m_list.Count; i++)
		{
			ELEMENT elem = m_list [i];
			elem.persionList.Remove(persionName);
		}

	}

	public List<Persion> GetPersionByFaction(String factionName)
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

	public int GetFactionsPower(String factionName)
	{
		int result = 0;
		for (int i = 0; i < m_list.Count; i++)
		{
			if (m_list [i].fationName != factionName) 
			{
				continue;
			}

			for (int j = 0; j < m_list[i].persionList.Count; j++)
			{
				Office office = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (m_list [i].persionList [j]);
				result += office.GetPower ();
			}
		}

		return result;
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