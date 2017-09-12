using System.Collections;
using System.Collections.Generic;
using System;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public delegate void DelegOnBtnClick();
	public DelegMsgBox m_delegMsgBox;

	public struct MsgBox
	{
		public string strDesc;
		public DelegOnBtnClick delegOnBtnClick;
	};

    public void NextTurn()
    {
		ArrayList arrObject = new ArrayList();
		arrObject.Add (new MsgBox{strDesc = "2222", delegOnBtnClick = OnBtnClick});

		m_delegMsgBox ("111", "22222", arrObject);
        //throw new EndGameException();
    }

	public void OnBtnClick()
	{
	}
}

public class EndGameException : System.Exception
{

}
