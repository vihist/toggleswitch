using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgFactory 
{
	public MyGame.MessageBox CreatePdt()
	{
		MyGame.MessageBox testMsg =  new MyGame.TestMessage();
		testMsg.RegeditOption();

		return testMsg;
	}
}
