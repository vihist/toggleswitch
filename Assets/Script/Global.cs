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

	public static GameData GetGameData()
	{
		return m_myGame.GetGameData ();
	}

	private static MyGame m_myGame; 
}


