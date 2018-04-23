using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
***SPAWNS ARE BASED ON THE CENTER OF THE SPRITES***
room locations
start 0 0 0 
final 200 0 0 
small 0 100 0 
medium 200 100 0 
large 400 100 0 
 
for reference - corner positions
Vector3 upperLeft = new Vector3 (((size - 1f) * 200f) + (wallWidth), 100f - (wallWidth), 0f);
Vector3 upperRight = new Vector3 (((size - 1f) * 200f + (wallWidth) + ((size * 2f) * (tileWidth))),
100f - (wallWidth), 0f);
Vector3 bottomRight = new Vector3 (((size - 1f) * 200f + (wallWidth) + ((size * 2f) * (tileWidth))),
100f - (wallWidth) - ((size * 2f) * (tileWidth)), 0f);
Vector3 bottomLeft = new Vector3 (((size - 1f) * 200f) + (wallWidth),
100f - (wallWidth) - ((size * 2f) * (tileWidth)), 0f);
*/ 

public class ItemManager : MonoBehaviour {

	public int room,dim;
	public Vector3 playerSpawn, exitLocation, spawn;
	public GameObject[] itemTypes = new GameObject[5];
	public int numOfItems;
	public const float squareLength = 3.2f; //width/length of one square
	public const float wallWidth = 6.4f; //width of the wall prefabs
	public const float tileWidth = 9.6f; //width of one floor tile prefab (3 squares x 3 squares)

	public int x = 0;
	public int y = 0;

	public int[,] possibleSpawns;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		this.room = GetComponentInParent<GameManager>().currentRoom;
		if( (GetComponentInParent<GameManager>().spawnItems) && (room != -2) )
		{
			spawnItems(room);
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

	void spawnItems(int size)
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

			dim = ((room*2)+1)*3;
			possibleSpawns = new int [dim,dim];
			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					possibleSpawns[i,j] = 0;
				}
			}
			
			this.numOfItems = Random.Range((size*2)+2,(size*2)+5);
			//this.numOfItems = 1;
			bool spawnAllowed;
			for (int i = 0; i < numOfItems; i++)
			{
				spawnAllowed = false;
				while (!spawnAllowed)
				{
					//grab a random spawn point within the room
					x = Random.Range(0,dim);
					y = Random.Range(0,dim);
					//create spawn based on random x, y and the size
					spawn = new Vector3(((size-1f)*200) + wallWidth + (squareLength/2) + (x*squareLength), 
								100f - wallWidth - (squareLength/2) - (y*squareLength), 0f);
					//check the spawn to see if it works
					spawnAllowed = checkSpawn();
				}
				possibleSpawns[x,y] = 1;
				Instantiate(itemTypes[Random.Range(0,itemTypes.Length)], spawn,
					Quaternion.identity,this.transform);
			}
		}
		GetComponentInParent<GameManager>().spawnItems = false;
	}

	bool checkSpawn ()
	{
		//check if there is already an item there
		if (possibleSpawns[x,y] == 1) return false;
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
		//dont allow spawns where the room's exit is located
		if (spawn == this.exitLocation + new Vector3(4.8f,-4.8f,0f)) return false;
		//check if inside collision somehow
		else return true;
	}
}
