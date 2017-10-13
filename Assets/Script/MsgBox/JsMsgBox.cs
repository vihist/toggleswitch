using System;

public class JsMsgBox : MessageBox
{
	public JsMsgBox()
	{
		strTitile = "js";
		strContent = "js start";

		arrOption.Add(new Option { strDesc = "1111", delegOnBtnClick = OnOption1 });
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

