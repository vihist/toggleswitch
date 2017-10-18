using System;

public class JsMsgBox : MessageBox
{
	public JsMsgBox()
	{
		strTitile = m_cvs.Get("JSMSG", "TITLE");
		strContent = m_cvs.Get("JSMSG", "CONTENT");

		arrOption.Add(new Option { strDesc = m_cvs.Get("JSMSG", "OPT1"), delegOnBtnClick = OnOption1 });
	}

	public static bool PreCondition()
	{
		return Tools.Probability.Calc(50);
	}

	private void  OnOption1()
	{
		Global.GetGameData ().tx = 2000;
		//NextMsgBox (new TestMessage2());
	}
}

