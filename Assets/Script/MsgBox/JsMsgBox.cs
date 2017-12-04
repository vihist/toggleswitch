using System;
using Tools;

public class JsMsgBox : MessageBox
{
	public JsMsgBox()
	{
		strTitile = Cvs.MsgDesc.Get("JSMSG", "TITLE");
		strContent = Cvs.MsgDesc.Get("JSMSG", "CONTENT");

		arrOption.Add(new Option { strDesc =  Cvs.MsgDesc.Get("JSMSG", "OPT1"), delegOnBtnClick = OnOption1 });
	}

	public static bool PreCondition()
	{
		return Tools.Probability.Calc(100);
	}

	private void  OnOption1()
	{
		Global.GetGameData ().tm = 2000;
		//Global.GetGameData ().Init ();
		//NextMsgBox (new TestMessage2());
	}
}

