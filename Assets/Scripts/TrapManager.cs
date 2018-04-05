using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(GetComponentInParent<GameManager>().spawnTraps) 
		{
			spawnTraps(GetComponentInParent<GameManager>().currentRoom);
		}
		
		if (GetComponentInParent<GameManager>().complete)
		{
			print("traps reset");
			foreach(Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			Start();
		}
	}

	void spawnTraps(int size)
	{
		if (GetComponentInParent<GameManager>().currentRoom == -1) //start room - do nothing
		{
			print("trap spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == -2) 
		{
			print("trap spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 1)
		{
			print("trap spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 2)
		{
			print("trap spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 3)
		{
			print("trap spawn in room " + size);
		}
		GetComponentInParent<GameManager>().spawnTraps = false;
	}
}
