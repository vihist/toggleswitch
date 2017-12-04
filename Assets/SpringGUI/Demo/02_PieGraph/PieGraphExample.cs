
/*=========================================
* Author: springDong
* Description: pie graph example.
==========================================*/

using UnityEngine;
using System.Collections.Generic;
using SpringGUI;
using System;
using Tools;

public class PieGraphExample : MonoBehaviour
{
    public PieGraph PieGraph = null;

	struct Triplet
	{
		public int value;
		public String strName;
		public Color color;
	}

    private void Start()
    {
        // method 1:
        //PieGraph.Inject(new Pies(new List<PieData>()
        //     {
        //         new PieData(26 ,Color.white)
        //     }));

        // method 2:

		List<PieData> list = new List<PieData> ();
		foreach (Faction faction in Global.GetGameData().m_FactionDict.Values)
		{
			list.Add (new PieData (Global.GetGameData ().m_factionReleation.GetFactionsPower (faction.GetName ()), GetColor(faction.GetName ()), Cvs.UiDesc.Get(faction.GetFullName ())));
		}

		PieGraph.Inject(list);

        //// method 3:
        //PieGraph.Inject(
        //    new List<float>() { 12 , 10 } ,
        //    new List<Color>() { Color.blue , Color.black });

        //// method 4:
        //PieGraph.Inject(new List<float>() { 8 , 7 });
    }

	private Color GetColor(String str)
	{
		FACTION eFaction = (FACTION)Enum.Parse(typeof(FACTION), str);

		switch (eFaction)
		{
		case FACTION.ShiDF:
			return Color.green;
		case FACTION.XunG:
			return Color.red;
		default:
			return Color.black;
		}
	}
}