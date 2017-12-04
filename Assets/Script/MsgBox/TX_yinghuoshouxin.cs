using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

class TX_yinghuoshouxin : MessageBox
{
    public TX_yinghuoshouxin()
    {
		strTitile = Cvs.MsgDesc.Get("TX_YHSX", "TITLE");
		strContent = Cvs.MsgDesc.Get("TX_YHSX", "CONTENT");

        Persion taiChang = Global.GetGameData().m_officeResponse.GetPersionByOffice(OFFICE.TaiC.ToString());
        if (taiChang == null)
        {
			arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX", "OPT5"), delegOnBtnClick = OnOption5 });
            return;
        }

        Persion chengXiang = Global.GetGameData().m_officeResponse.GetPersionByOffice(OFFICE.ChengX.ToString());
        if (chengXiang != null)
        {
			arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX", "OPT1"), delegOnBtnClick = OnOption1 });
        }

		arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX", "OPT2"), delegOnBtnClick = OnOption2 });
		arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX", "OPT3"), delegOnBtnClick = OnOption3 });
		arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX", "OPT4"), delegOnBtnClick = OnOption4 });
    }

    public static bool PreCondition()
    {
        return Tools.Probability.Calc(100);
    }

    private void OnOption1()
    {
        Global.GetGameData().tm = 5000;



        if (Tools.Probability.Calc(100))
        {

        }

        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption2()
    {
        Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption3()
    {
        Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption4()
    {
        Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }

    private void OnOption5()
    {
        Global.GetGameData().tm--;
        Global.GetGameData().m_Emperor.despress++;
    }

}
