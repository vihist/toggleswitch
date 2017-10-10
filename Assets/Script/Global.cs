using System;

public class Global
{
	public static void SetMyGame(MyGame myGame)
	{
		m_myGame = myGame;
	}

	public static MyGame GetMyGame()
	{
		return m_myGame;
	}

	private static MyGame m_myGame; 
}


