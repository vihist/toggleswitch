using System;

namespace Tools
{
	public class Probability
	{
		public static bool Calc (int iRate)
		{
			Random ran=new Random();
			int RandKey=ran.Next(1,100);
			if(RandKey <= iRate)
			{
				return true;
			}

			return false;
		}
	}
}

