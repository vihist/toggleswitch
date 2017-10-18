using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MyGame
{
	public delegate void DelegMsgBox(string strTitle, string strContent, ArrayList arrOption);
	public DelegMsgBox m_delegPopMsgBox;

	public MyGame(GameData gameData = null)
	{
		m_bEnd = false;
		m_ListMessageBox = new List<MessageBox>();
		m_msgGenerater = new Generater ();

		m_GameData = gameData;
		if (m_GameData == null) 
		{
			m_GameData = new GameData ();
		}

		m_msgGenerater.Register(typeof(TestMessage));
		m_msgGenerater.Register (typeof(JsMsgBox));
	}

	public bool IsEnd()
	{
		return m_bEnd;
	}

    public void NextTurn()
    {
        m_ListMessageBox.Clear();

		ArrayList lstMsgBox = m_msgGenerater.Generate ();

		foreach(object obj in lstMsgBox)
		{
			MessageBox testMsg =  obj as MessageBox;
			if (testMsg != null)
			{
				m_ListMessageBox.Add(testMsg);
			} 
		}
    }

	public GameData GetGameData()
	{
		return m_GameData;
	}

    private bool m_bEnd;
	private Generater m_msgGenerater;
	private GameData m_GameData;
    public List<MessageBox> m_ListMessageBox;
}

[Serializable]
public class GameData
{

	public GameData()
	{
		tx = 10;
	}

	public int tx;
}