using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

class CT_zhongchenyiwaisiwang : MessageBox
{
    public CT_zhongchenyiwaisiwang()
    {
		strTitile = Cvs.MsgDesc.Get("CT_ZCYWSW", "TITLE");
		strContent = Cvs.MsgDesc.Get("CT_ZCYWSW", "CONTENT");

		arrOption.Add(new Option { strDesc = Cvs.MsgDesc.Get("CT_ZCYWSW", "OPT1"), delegOnBtnClick = OnOption1 });
    }

    void OnOption1()
    {

    }
}
