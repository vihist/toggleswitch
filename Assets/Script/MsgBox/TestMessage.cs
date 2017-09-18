﻿using System;
using System.Collections.Generic;

public class TestMessage : MessageBox
{
    public TestMessage()
    {
        strTitile = "test title1";
        strContent = "test content1";

        arrOption.Add(new Option { strDesc = "1111", delegOnBtnClick = OnOption1 });
    }

    public static bool PreCondition()
    {
        return true;
    }

	private void  OnOption1()
    {
		NextMsgBox (new TestMessage2());
    }
}

public class TestMessage2 : MessageBox
{
	public TestMessage2()
	{
		strTitile = "test title2";
		strContent = "test content2";

		arrOption.Add(new Option { strDesc = "2222", delegOnBtnClick = OnOption1 });
	}

	public static bool PreCondition()
	{
		return true;
	}

	private void  OnOption1()
	{
		Console.WriteLine("OnOption1");
	}
}