using System;
using System.Collections;
using System.Collections.Generic;
using Tools;

public class CT_SanggongKongQue : MessageBox
{

	public CT_SanggongKongQue()
	{
		m_lstSelectPersion = new List<Persion> ();

		strTitile = Cvs.MsgDesc.Get("CT_SGKQ", "TITLE");
		strContent = String.Format(Cvs.MsgDesc.Get("CT_SGKQ", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()));

		int[] a = {1,2,4};

		DelegOnBtnClick[] delegArray = {OnOption1, OnOption2, OnOption3, OnOption4, OnOption5};


		int index = 0;
		foreach (FACTION faction in Enum.GetValues(typeof(FACTION)))
		{
			List<Persion> lsrPersion = Global.GetGameData ().m_factionReleation.GetPersionByFaction(faction.ToString());
			if (lsrPersion.Count == 0) 
			{
				continue;
			}

			lsrPersion.Sort ((x, y)=> -x.m_score.CompareTo(y.m_score));

			int iIndex = 0;
			for(int i=0; i<lsrPersion.Count; i++)
			{
				Office office = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (lsrPersion [i].GetName ());
				if (office.GetEnum() > m_office) 
				{
					iIndex = i;
					break;
				}
			}

			Persion persion = lsrPersion [iIndex];

			Office  curOffice = Global.GetGameData ().m_officeResponse.GetOfficeByPersion (persion.GetName ());

			arrOption.Add(new Option { 
				strDesc = String.Format(Cvs.MsgDesc.Get("CT_SGKQ", "OPT1"), Cvs.UiDesc.Get(curOffice.GetName()), persion.GetName(), Cvs.UiDesc.Get(Global.GetGameData().m_FactionDict[faction.ToString()].GetFullName())), 
				delegOnBtnClick = delegArray[index]});

			m_lstSelectPersion.Add (persion);
			index++;
		}

	}

	public static bool PreCondition()
	{
		for(int i=0; i<3; i++)
		{
			OFFICE office = (OFFICE)i;
			Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice (office.ToString());
			if(persion == null)
			{
				m_office = office;
				return true;
			}
		}

		return false;
	}

	private void OnOption1()
	{
		int i = 0;
		OnSelect (i);
	}

	private void OnOption2()
	{
		int i = 1;
		OnSelect (i);
	}

	private void OnOption3()
	{
		int i = 2;
		OnSelect (i);
	}

	private void OnOption4()
	{
		int i = 3;
		OnSelect (i);
	}

	private void OnOption5()
	{
		int i = 4;
		OnSelect (i);
	}

	private void OnSelect(int i)
	{
		Persion persion = m_lstSelectPersion [i];
		Global.GetGameData ().m_officeResponse.Set (m_office.ToString(), persion.m_name);
	}


	private static OFFICE? m_office = null;
	private List<Persion> m_lstSelectPersion;
}


