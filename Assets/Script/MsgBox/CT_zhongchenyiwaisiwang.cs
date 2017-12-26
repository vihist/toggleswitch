using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

class CT_zhongchenyiwaisiwang : MessageBox
{
	public CT_zhongchenyiwaisiwang(OFFICE office)
    {
		m_office = office;

        Persion persion = Global.GetGameData().m_officeResponse.GetPersionByOffice(m_office.ToString());

        strTitile = Cvs.MsgDesc.Get("CT_ZCYWSW", "TITLE");
        strContent = String.Format(Cvs.MsgDesc.Get("CT_ZCYWSW", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()), persion.GetName());  

        arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("CT_ZCYWSW", "OPT1"), delegOnBtnClick = OnOption1 });

    }

	void OnOption1(int i)
    {
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice (m_office.ToString());
		persion.Die ();

		Global.GetGameData ().tm--;
    }

	private OFFICE m_office;
}
