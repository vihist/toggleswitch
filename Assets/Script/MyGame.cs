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
		m_msgFactory = new MsgFactory ();
    }

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        m_ListMessageBox.Clear();

		MessageBox testMsg = m_msgFactory.CreatePdt ();

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

    public class TestMessage : MessageBox
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
	private MsgFactory m_msgFactory;
    public List<MessageBox> m_ListMessageBox;
}
