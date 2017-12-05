using System;
using Tools;

class CT_ZhongchenBingCi : MessageBox
{
	public CT_ZhongchenBingCi(OFFICE office)
	{
		m_office = office;

		strTitile = Cvs.MsgDesc.Get("CT_ZCYWSW", "TITLE");
		strContent = Cvs.MsgDesc.Get("CT_ZCYWSW", "CONTENT");

		arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("CT_ZCYWSW", "OPT1"), delegOnBtnClick = OnOption1 });
	}

	void OnOption1()
	{
		Persion persion = Global.GetGameData ().m_officeResponse.GetPersionByOffice (m_office.ToString());
		persion.Quit ();
	}

	private OFFICE m_office;
}