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
	
        Persion taiChang = Global.GetGameData().m_officeResponse.GetPersionByOffice(OFFICE.TaiC.ToString());
        if (taiChang == null)
        {
			strContent = Cvs.MsgDesc.Get("TX_YHSX_TXKQ", "CONTENT");
			arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("TX_YHSX_TXKQ", "OPT1"), delegOnBtnClick = OnOption5 });
            return;
        }

		strContent = String.Format (Cvs.MsgDesc.Get ("TX_YHSX", "CONTENT"), taiChang.GetName ());

		for (int i=0; i<3; i++)
		{
			OFFICE eOffice = (OFFICE)i;
			Persion persion = Global.GetGameData().m_officeResponse.GetPersionByOffice(eOffice.ToString());
			if(persion == null)
			{
				continue;
			}

			if (Global.GetGameData ().m_factionReleation.GetFactionByPersion (persion.GetName ()) != Global.GetGameData ().m_factionReleation.GetFactionByPersion (taiChang.GetName ())) 
			{
				effOffice = eOffice;
				break;
			}

			if (i == 2) 
			{
				effOffice = eOffice;
				break;
			}
		}

		if (effOffice != null)
        {
			arrOption.Add(new Option { strDesc = String.Format(Cvs.MsgDesc.Get("TX_YHSX", "OPT1"), Cvs.UiDesc.Get(effOffice.ToString())), delegOnBtnClick = OnOption1 });
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
        //Global.GetGameData().tm = 5000;

        if (Tools.Probability.Calc(50))
        {
			NextMsgBox (new CT_zhongchenyiwaisiwang((OFFICE)effOffice));
        }
		else
		{
			NextMsgBox (new CT_ZhongchenBingCi((OFFICE)effOffice));
		}

        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption2()
    {
        //Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption3()
    {
        //Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }
    private void OnOption4()
    {
        //Global.GetGameData().tm = 2000;
        //Global.GetGameData ().Init ();
        //NextMsgBox (new TestMessage2());
    }

    private void OnOption5()
    {
        Global.GetGameData().tm--;
        Global.GetGameData().m_Emperor.despress++;
    }

	private OFFICE? effOffice = null;

}
