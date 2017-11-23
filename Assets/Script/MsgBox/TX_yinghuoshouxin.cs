using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TX_yinghuoshouxin : MessageBox
{
    public TX_yinghuoshouxin()
    {
        strTitile = m_cvs.Get("TX_YHSX", "TITLE");
        strContent = m_cvs.Get("TX_YHSX", "CONTENT");

        arrOption.Add(new Option { strDesc = m_cvs.Get("TX_YHSX", "OPT1"), delegOnBtnClick = OnOption1 });
        arrOption.Add(new Option { strDesc = m_cvs.Get("TX_YHSX", "OPT2"), delegOnBtnClick = OnOption1 });
        arrOption.Add(new Option { strDesc = m_cvs.Get("TX_YHSX", "OPT3"), delegOnBtnClick = OnOption1 });
		arrOption.Add(new Option { strDesc = m_cvs.Get("TX_YHSX", "OPT4"), delegOnBtnClick = OnOption1 });
    }

    public static bool PreCondition()
    {
        return Tools.Probability.Calc(100);
    }

    private void OnOption1()
    {
        Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
}
