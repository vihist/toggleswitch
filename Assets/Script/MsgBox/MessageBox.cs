using System.Collections;
using System.Collections.Generic;
using Tools;

public delegate void DelegOnBtnClick();

public struct Option
{
    public string strDesc;
    public DelegOnBtnClick delegOnBtnClick;
};

public abstract class MessageBox
{
    public MessageBox()
    {
        arrOption = new ArrayList();
		m_listNext = new List<MessageBox> ();
    }


    public string strTitile;
    public string strContent;
    public ArrayList arrOption;

	protected void NextMsgBox(MessageBox msgBox)
	{
		m_listNext.Add (msgBox);
	}

	public List<MessageBox> m_listNext;
}
