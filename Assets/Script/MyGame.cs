﻿using System.Collections;
using System.Collections.Generic;
using System;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public DelegMsgBox m_delegPopMsgBox;

    public MyGame()
    {
        m_bEnd = false;
        m_ListMessageBox = new List<MessageBox>();
        m_msgGenerater = new Generater ();

        m_msgGenerater.Register(typeof(TestMessage));
    }

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        m_ListMessageBox.Clear();

		MessageBox testMsg =  m_msgGenerater.Generate () as MessageBox;
        if (testMsg != null)
        {
            testMsg.RegeditOption();
            m_ListMessageBox.Add(testMsg);
        } 
    }

    private bool m_bEnd;
	private Generater m_msgGenerater;
    public List<MessageBox> m_ListMessageBox;
}
