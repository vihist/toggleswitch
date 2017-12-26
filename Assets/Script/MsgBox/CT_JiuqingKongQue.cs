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

        Dictionary<String, List<Persion>> factionDict = new Dictionary<string, List<Persion>>();
        foreach (Persion p in cishiList)
        {
            if (p.GetOffice().GetEnum() == OFFICE.TaiC)
            {
                continue;
            }

            String factionName = p.GetFaction().GetName();
            if (!factionDict.ContainsKey(factionName))
            {
                factionDict.Add(factionName, new List<Persion>());

            }
            factionDict[factionName].Add(p);
        }

        List<Persion> selectPersionList = new List<Persion>();
        foreach (List<Persion> persionList in factionDict.Values)
        {
            persionList.Sort((x, y) => -x.m_score.CompareTo(y.m_score));
            selectPersionList.Add(persionList[0]);
        }

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



