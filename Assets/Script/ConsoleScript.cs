using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConsoleScript : MonoBehaviour 
{
	public Vector3 pos;
	public int RoomNumber = 1;
	public Text TextADroite;
	
	public void ActivateText()
	{
		switch (RoomNumber)
		{
			case 1 :
				Room1();
				break;
			case 2 :
				Room2();
				break;
			case 3 :
				Room3();
				break;
			case 4 :
				Room4();
				break;
			case 5 :
				Room5();
				break;
			case 6 :
				Room6();
				break;
		}
	}

	public void Room1()
	{
		var textToWrite = "Attaque Spéciale Room1";
		TextADroite.text = textToWrite;
	}
	
	public void Room2()
	{
		var textToWrite = "Attaque Spéciale Room2";
		TextADroite.text = textToWrite;
	}
	
	public void Room3()
	{
		var textToWrite = "Attaque Spéciale Room3";
		TextADroite.text = textToWrite;
	}
	
	public void Room4()
	{
		var textToWrite = "Attaque Spéciale Room4";
		TextADroite.text = textToWrite;
	}
	
	public void Room5()
	{
		var textToWrite = "Attaque Spéciale Room5";
		TextADroite.text = textToWrite;
	}
	
	public void Room6()
	{
		var textToWrite = "Attaque Spéciale Room6";
		TextADroite.text = textToWrite;
	}
}
