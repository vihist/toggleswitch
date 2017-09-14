using System.Collections;
using System.Collections.Generic;

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
    }

    public string strTitile;
    public string strContent;
    public ArrayList arrOption;

    public abstract void RegeditOption();
}
