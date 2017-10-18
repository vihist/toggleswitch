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
		return Tools.Probability.Calc(100);
	}

	private void  OnOption1()
	{
		Global.GetGameData ().tm = 2000;
		Global.GetGameData ().Init ();
		//NextMsgBox (new TestMessage2());
	}
}

