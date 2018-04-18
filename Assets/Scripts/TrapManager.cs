using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

	public int room;
	public Vector3 playerSpawn, exitLocation, spawn;
	public GameObject[] trapTypes = new GameObject[1];
	public int numOfTraps;
	public const float squareLength = 3.2f; //width/length of one square
	public const float wallWidth = 6.4f; //width of the wall prefabs
	public const float tileWidth = 9.6f; //width of one floor tile prefab (3 squares x 3 squares)

	public int x = 0;
	public int y = 0;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		this.room = GetComponentInParent<GameManager>().currentRoom;
		if(GetComponentInParent<GameManager>().spawnTraps) 
		{
			spawnTraps(room);
		}
		
		if (GetComponentInParent<GameManager>().reset)
		{
			foreach(Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
				Start();
			}
		}
	}

	void spawnTraps(int size)
	{
		if (room == -1) //start room - do nothing
		{
			this.playerSpawn = GetComponentInParent<GameManager>().startSpawn;
			this.exitLocation = GetComponentInParent<GameManager>().start.GetComponent<RoomBuilder>().exitPosition 
				+ new Vector3(4.8f,-4.8f,0f);
		}
		else if (room == -2) 
		{
			this.playerSpawn = GetComponentInParent<GameManager>().finalSpawn;
			this.exitLocation = GetComponentInParent<GameManager>().final.GetComponent<RoomBuilder>().exitPosition 
				+ new Vector3(4.8f,-4.8f,0f);
		}
		else //in rooms 1, 2, or 3
		{
			if(size == 1) 
			{
				this.playerSpawn = GetComponentInParent<GameManager>().smallSpawn;
				this.exitLocation = GetComponentInParent<GameManager>().small.GetComponent<RoomBuilder>().exitPosition 
					+ new Vector3(4.8f,-4.8f,0f);
			}
			if (size == 2) 
			{
				this.playerSpawn = GetComponentInParent<GameManager>().mediumSpawn;
				this.exitLocation = GetComponentInParent<GameManager>().medium.GetComponent<RoomBuilder>().exitPosition 
					+ new Vector3(4.8f,-4.8f,0f);
			}
			if (size == 3) 
			{
				this.playerSpawn = GetComponentInParent<GameManager>().largeSpawn;
				this.exitLocation = GetComponentInParent<GameManager>().large.GetComponent<RoomBuilder>().exitPosition 
					+ new Vector3(4.8f,-4.8f,0f);
			}
			
			this.numOfTraps = Random.Range((size*2),(size*2)+3);
			//this.numOfItems = 1;
			bool spawnAllowed;
			for (int i = 0; i < numOfTraps; i++)
			{
				spawnAllowed = false;
				while (!spawnAllowed)
				{
					//grab a random spawn point within the room
					x = Random.Range(0,GetComponentInParent<GameManager>().dim);
					y = Random.Range(0,GetComponentInParent<GameManager>().dim);
					//create spawn based on random x, y and the size
					spawn = new Vector3(((size-1f)*200) + wallWidth + (squareLength/2) + (x*squareLength), 
								100f - wallWidth - (squareLength/2) - (y*squareLength), 0f);
					//check the spawn to see if it works
					spawnAllowed = checkSpawn();
				}
				GetComponentInParent<GameManager>().possibleSpawns[x,y] = 1;
				Instantiate(trapTypes[Random.Range(0,trapTypes.Length)], spawn,
					Quaternion.identity,this.transform);
			}
		}
		GetComponentInParent<GameManager>().spawnTraps = false;
	}

	bool checkSpawn ()
	{
		//check if there is already a trap there
		if (GetComponentInParent<GameManager>().possibleSpawns[x,y] == 1) return false;
		//dont allow spawns within one space of player's spawn
		if (spawn == this.playerSpawn + new Vector3(1.6f,-1.6f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(1.6f,1.6f,0f))  return false;
		if (spawn == this.playerSpawn + new Vector3(-1.6f,-1.6f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(-1.6f,1.6f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(4.8f,-4.8f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(4.8f,-1.6f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(4.8f,1.6f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(-1.6f,-4.8f,0f)) return false;
		if (spawn == this.playerSpawn + new Vector3(1.6f,-4.8f,0f)) return false;
		//dont allow spawns within one space of the room's exit
		if (spawn == this.exitLocation + new Vector3(4.8f,-4.8f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(1.6f,-1.6f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(1.6f,-4.8f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(1.6f,-8.0f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(4.8f,-1.6f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(4.8f,-8.0f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(8.0f,-1.6f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(8.0f,-4.8f,0f)) return false;
		if (spawn == this.exitLocation + new Vector3(8.0f,-8.0f,0f)) return false;
		//check if inside collision somehow
		else return true;
	}
}
