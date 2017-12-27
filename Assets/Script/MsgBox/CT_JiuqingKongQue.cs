using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

public class CT_JiuqingKongQue : MessageBox
{

    public CT_JiuqingKongQue()
    {

        strTitile = Cvs.MsgDesc.Get("CT_JQKQ", "TITLE");
        strContent = String.Format(Cvs.MsgDesc.Get("CT_JQKQ", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()));

        m_lstSelectPersion = GetSelectionPersion();

        int index = 0;
        foreach (Persion p in m_lstSelectPersion)
        {
            arrOption.Add(new Option
            {
                strDesc = String.Format(Cvs.MsgDesc.Get("CT_JQKQ", "OPT1"), Cvs.UiDesc.Get(p.GetOffice().GetName()), p.GetName(), Cvs.UiDesc.Get(p.GetFaction().GetFullName())),
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

        List<OFFICE> offList = Global.GetGameData().GetOfficeEnum(OFFICE_GROUP.JiuQing);
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
        List<Persion> cishiList = Global.GetGameData().GetPersionByOfficeGroup(OFFICE_GROUP.CiShi);
		cishiList.Sort (
			delegate(Persion x, Persion y) 
			{ 
				int xScore = x.m_score;
				int yScore = y.m_score;

				Persion persionChengx = Global.GetGameData ().GetPersionByOffice (OFFICE.ChengX);
				if(x.GetFaction() != persionChengx.GetFaction())
				{
					xScore = (int)(xScore*0.8);
				}
				if(y.GetFaction() != persionChengx.GetFaction())
				{
					yScore = (int)(yScore*0.8);
				}
				return -(xScore.CompareTo(yScore));
			});

		List<Persion> selectPersionList = cishiList.GetRange(0, Math.Min(3, cishiList.Count()));
		return selectPersionList;
    }

    private void OnSelect(int i)
    {
        Persion persion = m_lstSelectPersion[i];
        Global.GetGameData().m_officeResponse.Set(m_office.ToString(), persion.m_name);
    }

    private static OFFICE? m_office = null;
    private List<Persion> m_lstSelectPersion;
}



