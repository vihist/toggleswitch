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

	public static GameEnv GetEnv()
	{
		return m_GameEnv;
	}

	public static void SetEnv(GameEnv env )
	{
		m_GameEnv = env;
	}

	private static MyGame m_myGame = null; 
	private static GameEnv m_GameEnv = null;
}


