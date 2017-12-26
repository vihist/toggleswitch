using System;
using Tools;

class CT_ZhongchenBingCi : MessageBox
{
	public CT_ZhongchenBingCi(OFFICE office)
	{
		m_office = office;

        Persion persion = Global.GetGameData().m_officeResponse.GetPersionByOffice(m_office.ToString());

        strTitile = Cvs.MsgDesc.Get("CT_ZCBC", "TITLE");
        strContent = String.Format(Cvs.MsgDesc.Get("CT_ZCBC", "CONTENT"), Cvs.UiDesc.Get(m_office.ToString()), persion.GetName());

        arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("CT_ZCBC", "OPT1"), delegOnBtnClick = OnOption1 });
    }

	void OnOption1(int i)
	{
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice (m_office.ToString());
		persion.Quit ();
	}

	private OFFICE m_office;
}