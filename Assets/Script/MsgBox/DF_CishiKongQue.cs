using System;
using System.Collections;
using System.Collections.Generic;
using Tools;


public class DF_CishiKongQue : MessageBox
{
	public DF_CishiKongQue ()
	{

		strTitile = Cvs.MsgDesc.Get("DF_CSKQ", "TITLE");
		strContent = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()));

		m_lstSelectPersion = GetSelectionPersion();

		int index = 0;
		foreach (Persion p in m_lstSelectPersion)
		{
			arrOption.Add(new Option
				{
					strDesc = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "OPT1"), Cvs.UiDesc.Get(p.GetOffice().GetName()), p.GetName(), Cvs.UiDesc.Get(p.GetFaction().GetFullName())),
					delegOnBtnClick = OnSelect
				});

			index++;
		}
	}

	public static bool PreCondition()
	{
		Persion persionChengx = Global.GetGameData ().GetPersionByOffice (OFFICE.ChengX);
		if (persionChengx == null)
		{
			return false;
		}

		List<OFFICE> offList = Global.GetGameData().GetOfficeEnum(OFFICE_GROUP.CiShi);
		foreach (OFFICE eOffice in offList)
		{
			Persion persion = Global.GetGameData().m_officeResponse.GetPersionByOffice(eOffice.ToString());
			if (persion == null)
			{
				m_office = eOffice;
				return true;
			}
		}

		return false;
	}

	private List<Persion> GetSelectionPersion()
	{
		Persion persionChengx = Global.GetGameData ().GetPersionByOffice (OFFICE.ChengX);

		List<Persion> selectPersionList = new List<Persion> ();
		for (int i = 0; i < 3; i++) 
		{
			Persion newPersion = new Persion (Persion.GetMaleName (), 0);
		
			if (Tools.Probability.Calc (70)) 
			{
				m_faction = persionChengx.GetFaction ().GetName();
			} 
			else 
			{
				m_faction = ((FACTION)(Tools.Probability.GetRandomNum(0, 2))).ToString();
			}

			selectPersionList.Add (newPersion);
		}

		return selectPersionList;
	}

	private void OnSelect(int i)
	{
		Persion persion = m_lstSelectPersion[i];
		Global.GetGameData().m_officeResponse.Set(m_office.ToString(), persion.m_name);
		Global.GetGameData ().m_factionReleation.Set (persion.GetName(), m_faction.ToString());
	}

	private static OFFICE? m_office = null;
	private static String m_faction = null;
	private List<Persion> m_lstSelectPersion;
}