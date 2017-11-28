
/*=========================================
* Author: springDong
* Description: pie graph example.
==========================================*/

using UnityEngine;
using System.Collections.Generic;
using SpringGUI;
using System;

public class PieGraphExample : MonoBehaviour
{
    public PieGraph PieGraph = null;

    private void Start()
    {
        // method 1:
        //PieGraph.Inject(new Pies(new List<PieData>()
        //     {
        //         new PieData(26 ,Color.white)
        //     }));

        // method 2:

		List<int> list = new List<int> ();
		foreach (Faction faction in Global.GetGameData().m_FactionDict.Values)
		{
			list.Add (Global.GetGameData ().m_factionReleation.GetFactionsPower (faction.GetName ()));
		}

        PieGraph.Inject(new List<PieData>()
        {
				new PieData(list[0],Color.magenta),
				new PieData(list[1] ,Color.red),
        });

        //// method 3:
        //PieGraph.Inject(
        //    new List<float>() { 12 , 10 } ,
        //    new List<Color>() { Color.blue , Color.black });

        //// method 4:
        //PieGraph.Inject(new List<float>() { 8 , 7 });
    }
}