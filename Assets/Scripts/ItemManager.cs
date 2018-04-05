using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(GetComponentInParent<GameManager>().spawnItems) 
		{
			spawnItems(GetComponentInParent<GameManager>().currentRoom);
		}
		
		if (GetComponentInParent<GameManager>().complete)
		{
			print("items reset");
			foreach(Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			Start();
		}
	}

	void spawnItems(int size)
	{
		if (GetComponentInParent<GameManager>().currentRoom == -1) //start room - do nothing
		{
			print("item spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == -2) 
		{
			print("item spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 1)
		{
			print("item spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 2)
		{
			print("item spawn in room " + size);
		}
		else if (GetComponentInParent<GameManager>().currentRoom == 3)
		{
			print("item spawn in room " + size);
		}
		GetComponentInParent<GameManager>().spawnItems = false;
	}
}
