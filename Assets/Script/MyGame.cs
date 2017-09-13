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
        m_ListMessageBox.Clear();

        MessageBox testMsg = new TestMessage();
        testMsg.RegeditOption();

        m_ListMessageBox.Add (testMsg);
  
    }

    public abstract class MessageBox
    {
        public MessageBox()
        {
            arrOption = new ArrayList();
        }

        public string strTitile;
		public string strContent;
		public ArrayList arrOption;

        public abstract void RegeditOption();
    }

    class TestMessage : MessageBox
    {
        public TestMessage()
        {
            strTitile = "test title";
            strContent = "test content";    
        }

        public override void RegeditOption()
        {
            arrOption.Add(new Option { strDesc = "2222", delegOnBtnClick = OnOption1 });
        }

        private void OnOption1()
        {
            Console.WriteLine("OnOption1");
        }
    }

    private bool m_bEnd;
    public List<MessageBox> m_ListMessageBox;
}
