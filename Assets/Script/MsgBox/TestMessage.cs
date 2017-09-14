using System;
using System.Collections.Generic;

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

    public static bool PreCondition()
    {
        return true;
    }

    private void OnOption1()
    {
        Console.WriteLine("OnOption1");
    }
}