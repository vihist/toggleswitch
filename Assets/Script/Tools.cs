using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tools
{
	public class Probability
	{
        public static bool IsProbOccur(double prob)
        {
            int prb = (int)(prob * 10000);

            System.Random ra = new System.Random(seed++);
            int result = ra.Next(1, 10000);
            if (result <= prb)
            {
                return true;
            }

            return false;
        }

        public static int GetRandomNum(int min, int max)
        {

            System.Random ra = new System.Random(seed + (int)DateTime.Now.Ticks);
            seed += 1000;
            return ra.Next(min, max);

        }

        public static int GetGaussianRandomNum(int min, int max)
        {
            System.Random ra = new System.Random(seed + (int)DateTime.Now.Ticks);
            seed += 1000;

            int[] iResult = { ra.Next(min, max), ra.Next(min, max), ra.Next(min, max) };

            return (iResult[0] + iResult[1] + iResult[2]) / 3;
        }

        public static bool Calc(int iRate)
        {
            System.Random ran = new System.Random(seed++);
            int RandKey = ran.Next(1, 100);
            if (RandKey <= iRate)
            {
                return true;
            }

            return false;
        }

		public static List<int> GetRandomNumArrayWithStableSum(int count, int sum)
		{
			List<int> list = new List<int> ();
			while(list.Count != count-1)
			{
				int random = GetRandomNum (0, sum);
				if (list.Contains (random)) 
				{
					continue;
				}

				list.Add (random);
			}

			list.Sort ();

			List<int> resultList = new List<int> ();
			for (int i = 0; i < list.Count+1; i++)
			{
				if (i == 0) 
				{
					resultList.Add (list [i] - 0);
				} 
				else if (i == list.Count-1)
				{
					resultList.Add (100 - list [i - 1]);
				}
				else
				{
					resultList.Add (list [i] - list [i - 1]);
				}
			}

			return resultList;
		}

        static int seed = 1;
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

        public int RowLength()
        {
            return m_rowIndex.Length;
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
