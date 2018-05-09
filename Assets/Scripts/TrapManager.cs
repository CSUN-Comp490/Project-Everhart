using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

	public int room,dim,levelsCompleted;
	public Vector3 playerSpawn, exitLocation, spawn;
	public GameObject[] trapTypes = new GameObject[10];
	public int numOfTraps;
	public const float squareLength = 3.2f; //width/length of one square
	public const float wallWidth = 6.4f; //width of the wall prefabs
	public const float tileWidth = 9.6f; //width of one floor tile prefab (3 squares x 3 squares)

	public int x = 0;
	public int y = 0;

	public int[,] possibleSpawns;
	
	public bool reset;

	void Start () 
	{
		reset = false;
	}
	
	void Update () 
	{
		this.levelsCompleted = GetComponentInParent<GameManager>().levelsComplete;
		this.room = GetComponentInParent<GameManager>().currentRoom;
		if( (GetComponentInParent<GameManager>().spawnTraps))
		{
			spawnTraps(room);
		}
		
		if (reset)
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
		}
		else if (room == -2) 
		{
			this.playerSpawn = GetComponentInParent<GameManager>().finalSpawn;
			this.exitLocation = GetComponentInParent<GameManager>().final.GetComponent<RoomBuilder>().exitPosition;

			//Spikes = trapTypes[0]
			//LArrow = trapTypes[4]
			//RArrow = trapTypes[6]
			//LRSaw = trapTypes[8]
			//UDSaw = trapTypes[10]

			//Instantiate(trapTypes[],new Vector3(f,f,f),Quaternion.identity,this.transform);
			//200,0,0
			Instantiate(trapTypes[0],new Vector3(208f,-40f,0f),Quaternion.identity,this.transform);
			Instantiate(trapTypes[0],new Vector3(272f,-40f,0f),Quaternion.identity,this.transform);
			Instantiate(trapTypes[0],new Vector3(240f,-27.2f,0f),Quaternion.identity,this.transform);
			Instantiate(trapTypes[0],new Vector3(240f,-52.8f,0f),Quaternion.identity,this.transform);
			if (levelsCompleted >= 1)
			{
				Instantiate(trapTypes[6],new Vector3(211.2f,-30.4f,0f),Quaternion.identity,this.transform);
				Instantiate(trapTypes[4],new Vector3(268.8f,-49.6f,0f),Quaternion.identity,this.transform);
			}
			if (levelsCompleted >= 3)
			{
				Instantiate(trapTypes[8],new Vector3(227.2f,-52.8f,0f),Quaternion.identity,this.transform);
				Instantiate(trapTypes[8],new Vector3(252.8f,-52.8f,0f),Quaternion.identity,this.transform);
			}
			if (levelsCompleted >= 5)
			{
				Instantiate(trapTypes[10],new Vector3(249.6f,-15.4f,0f),Quaternion.identity,this.transform);
				Instantiate(trapTypes[10],new Vector3(230.4f,-15.4f,0f),Quaternion.identity,this.transform);
			}
			if (levelsCompleted >= 7)
			{
				//Instantiate(trapTypes[10],new Vector3(f,f,0f),Quaternion.identity,this.transform);
				//Instantiate(trapTypes[10],new Vector3(f,f,0f),Quaternion.identity,this.transform);
				//Instantiate(trapTypes[4],new Vector3(f,f,0f),Quaternion.identity,this.transform);
				//Instantiate(trapTypes[6],new Vector3(f,f,0f),Quaternion.identity,this.transform);
			}
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
			
			this.numOfTraps = Random.Range((size*2)+levelsCompleted,(size*2)+3+levelsCompleted);
			//this.numOfItems = 1;
			bool spawnAllowed, inCollider;
			for (int i = 0; i < numOfTraps; i++)
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

					/*
					SpawnTester.transform.position = spawn;
					if (SpawnTester.GetComponent<SpawnTester>().inCollider)
					{	
						print("SpawnTester was inside a collider");
						spawnAllowed = false;
					}
					*/

					spawnAllowed = checkSpawn();
				}
				possibleSpawns[x,y] = 1;
				Instantiate(trapTypes[Random.Range(0,trapTypes.Length)], spawn,
					Quaternion.identity,this.transform);
			}
		}
		GetComponentInParent<GameManager>().spawnTraps = false;
	}

	bool checkSpawn ()
	{
		//check if there is already a trap there
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
