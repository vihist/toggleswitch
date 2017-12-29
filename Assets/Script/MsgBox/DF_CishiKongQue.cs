using System;
using System.Collections;
using System.Collections.Generic;
using Tools;


public class DF_CishiKongQue : MessageBox
{
	public DF_CishiKongQue ()
	{

		strTitile = Cvs.MsgDesc.Get("DF_CSKQ", "TITLE");

		Persion persionChengx = Global.GetGameData ().GetPersionByOffice (OFFICE.ChengX);
		Persion p1 = new Persion (Persion.GetMaleName (), 0);
		Faction f1 = persionChengx.GetFaction ();
		switch(f1.GetEnum())
		{
		case FACTION.WaiQ:
			{
				if (Tools.Probability.Calc (80))
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.ShiDF);
				}
				else
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.XunG);
				}
			}
			break;
		case FACTION.XunG:
			{
				if (Tools.Probability.Calc (70)) 
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.ShiDF);
				} 
				else 
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.XunG);
				}
			}
			break;
		case FACTION.ShiDF:
			{
				if (Tools.Probability.Calc (90)) 
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.ShiDF);
				} 
				else 
				{
					f1 = Global.GetGameData ().GetFaction (FACTION.XunG);
				}
			}
			break;
		default:
			break;
		}

		m_lstPersionWithFaction = new List<PersionWithFaction> ();
		m_lstPersionWithFaction.Add (new PersionWithFaction{persion = p1, faction = f1});

		strContent = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()), "", p1.GetName(), Cvs.UiDesc.Get(f1.GetFullName()));
		arrOption.Add(new Option{ strDesc = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "OPT1")), delegOnBtnClick = OnSelect });

		if (Tools.Probability.Calc (10)) 
		{
			
			Persion p2 = new Persion (Persion.GetMaleName (), 0);
			Faction f2 = null;
			if (f1 != Global.GetGameData ().GetFaction (FACTION.ShiDF)) 
			{
				f2 = Global.GetGameData ().GetFaction(FACTION.ShiDF);
			} 
			else 
			{
				f2 = Global.GetGameData ().GetFaction(FACTION.XunG);
			}

			m_lstPersionWithFaction.Add (new PersionWithFaction{persion = p2, faction = f2});

			arrOption.Add(new Option{ strDesc = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "OPT2"), p2.GetName(), Cvs.UiDesc.Get(f2.GetFullName())), delegOnBtnClick = OnSelect });
		}

		if (Global.GetGameData().m_persionWaitWQ != null) 
		{
			Persion p3 = Global.GetGameData().m_persionWaitWQ;
			Faction f3 = p3.GetFaction ();

			m_lstPersionWithFaction.Add (new PersionWithFaction{persion = p3, faction = f3});

			arrOption.Add(new Option{ strDesc = String.Format(Cvs.MsgDesc.Get("DF_CSKQ", "OPT3"), p3.GetName(), Cvs.UiDesc.Get(f3.GetFullName())), delegOnBtnClick = OnSelect });
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
		

	private void OnSelect(int i)
	{
		Persion persion = m_lstPersionWithFaction[i].persion;
		Faction faction = m_lstPersionWithFaction[i].faction;

        Global.GetGameData().m_MaleDict.Add(persion.GetName(), persion);
		Global.GetGameData ().m_factionReleation.Set (persion.GetName(), faction.GetName());
	}

	private static OFFICE? m_office = null;

	struct PersionWithFaction
	{
		public Persion persion;
		public Faction faction;
	}

	private List<PersionWithFaction> m_lstPersionWithFaction;
}