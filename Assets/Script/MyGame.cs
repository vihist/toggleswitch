using System.Collections;
using System.Collections.Generic;
using System;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public delegate void DelegOnBtnClick();
	public DelegMsgBox m_delegPopMsgBox;

    public struct Option
	{
		public string strDesc;
        public DelegOnBtnClick delegOnBtnClick;
	};

    public MyGame()
    {
        m_bEnd = false;
        m_ListMessageBox = new List<MessageBox>();
    }

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        ArrayList arrObject = new ArrayList();
		arrObject.Add (new Option { strDesc = "2222", delegOnBtnClick = OnBtnClick});

		m_ListMessageBox.Add (new MessageBox{strTitile = "111", strContent = "222", arrOption = arrObject});

        //MessageBox msgBox = new MessageBox("333", "444", arrObject);

        //m_delegPopMsgBox("333", "444", arrObject);

        //m_delegMsgBox ("111", "22222", arrObject);

        
    }

	public void OnBtnClick()
	{
        m_bEnd = true;


    }

    public class MessageBox
    {
        public string strTitile;
		public string strContent;
		public ArrayList arrOption;
    }

    private bool m_bEnd;
    public List<MessageBox> m_ListMessageBox;
}

public class EndGameException : System.Exception
{

}
