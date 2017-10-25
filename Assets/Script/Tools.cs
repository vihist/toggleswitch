using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tools
{
	public class Probability
	{
		public static bool Calc (int iRate)
		{
			System.Random ran=new System.Random();
			int RandKey=ran.Next(1,100);
			if(RandKey <= iRate)
			{
				return true;
			}

			return false;
		}
	}

	public class Cvs
	{
		public Cvs(string filename)
		{
			//读取csv二进制文件  
			TextAsset binAsset = Resources.Load (filename, typeof(TextAsset)) as TextAsset;         

			//读取每一行的内容  
			string [] lineArray = binAsset.text.Split ('\r');  

			m_colIndex = lineArray [0].Replace("ID,", "").Split (',');

			m_rowIndex = new string[lineArray.Length-1];
			m_ArrayData = new string [lineArray.Length-1][]; 

			for(int i =0; i < lineArray.Length-1; i++)  
			{  
				string[] raw = lineArray[i+1].Split (',');  
				m_rowIndex[i] = raw [0];

				m_ArrayData [i] = new string[raw.Length - 1];
				Array.Copy (raw, 1, m_ArrayData[i], 0, raw.Length-1);
			}  
		}

		public string Get(string row, string column)
		{
            try
            {
#if UNITY_EDITOR_OSX
			    return row+"_"+column;
#else
                int iRow = Array.FindIndex(m_rowIndex, s => s == row);
                int iCol = Array.FindIndex(m_colIndex, s => s == column);

                return m_ArrayData[iRow][iCol];
#endif
            }
            catch(Exception e)
            {
                Debug.Log(row + "," + column);
                throw;
            }
        }

		private string[] m_rowIndex;
		private string[] m_colIndex;
		private string[][] m_ArrayData;
	}
		
	[Serializable]
	public class SerialDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys;
		[SerializeField]
		List<TValue> values;

		public void OnBeforeSerialize()
		{
			keys = new List<TKey>(this.Keys);
			values = new List<TValue>(this.Values);
		}

		public void OnAfterDeserialize()
		{
			var count = Math.Min(keys.Count, values.Count);
			for (var i = 0; i < count; ++i)
			{
				this.Add(keys[i], values[i]);
			}
		}
	}
}
