using System;
using UnityEngine;

public class UIFrame 
{
	public static String GetUiDesc(string key)
	{
		return m_SceneCvs.Get (key, "CHI");
	}

	protected static Tools.Cvs m_SceneCvs = new Tools.Cvs ("text/uidesc");
}


