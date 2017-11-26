using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CT_zhongchenyiwaisiwang : MessageBox
{
    public CT_zhongchenyiwaisiwang()
    {
        strTitile = m_cvs.Get("CT_ZCYWSW", "TITLE");
        strContent = m_cvs.Get("CT_ZCYWSW", "CONTENT");

        arrOption.Add(new Option { strDesc = m_cvs.Get("CT_ZCYWSW", "OPT1"), delegOnBtnClick = OnOption1 });
    }

    void OnOption1()
    {

    }
}
