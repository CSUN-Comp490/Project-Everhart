using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	void Start () 
	{ 	
		
	}
	
	void Update () 
	{
		if(GetComponentInParent<GameManager>().spawnEnemies) 
		{
			spawnEnemies(GetComponentInParent<GameManager>().currentRoom);
		}
		
		if (GetComponentInParent<GameManager>().complete)
		{	
			print("enemies reset");
			foreach(Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			Start();
		}
	}

	void spawnEnemies(int size)
	{
		if (GetComponentInParent<GameManager>().currentRoom == -1) //start room - do nothing
		{
			print("enemy spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == -2) //spawn boss
		{
			print("enemy spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 1)
		{
			print("enemy spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 2)
		{
			print("enemy spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 3)
		{
			print("enemy spawn in room " + size);
		}
		GetComponentInParent<GameManager>().spawnEnemies = false;
	}
}
