using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public Camera camera;
	public GameObject player, start, final, small, medium, large, enemyManager, itemManager, trapManager;
	public Vector3 startSpawn,finalSpawn,smallSpawn,mediumSpawn,largeSpawn;

	//level building
	public int zeroPath, onePath, twoPath;
	public Vector3 zeroSpawn;
	public int zeroRoom;
	public Vector3[] oneSpawns = new Vector3[2];
	public int[] oneRooms = new int[2];
	public Vector3[] twoSpawns = new Vector3[3];
	public int[] twoRooms = new int[3];
	public int[] finalExitRooms = new int[2];
	public List<int> doors;
	public List<int> rooms;

	//tracking variables
	public bool complete;
	public int levelsComplete = 0;  //20 points 
	public int totalCurrency = 0;   //1 point
	public int roomsComplete = 0;   //5 points
	public int enemiesDefeated = 0; //5 points
	public int score = 0;

	public int currentRoom;
	public bool spawnEnemies, spawnItems, spawnTraps, reset;

	void Start () 
	{
		/*
		Instantiate(start,new Vector3(0,0,0),Quaternion.identity,this.transform);
		Instantiate(final,new Vector3(200,0,0),Quaternion.identity,this.transform);
		Instantiate(small,new Vector3(0,100,0),Quaternion.identity,this.transform);
		Instantiate(medium,new Vector3(200,100,0),Quaternion.identity,this.transform);
		Instantiate(large,new Vector3(400,100,0),Quaternion.identity,this.transform);
		*/
		//establish spawn locations for each room
		startSpawn = start.GetComponent<RoomBuilder>().spawn;
		finalSpawn = final.GetComponent<RoomBuilder>().spawn;
		smallSpawn = small.GetComponent<RoomBuilder>().spawn;
		mediumSpawn = medium.GetComponent<RoomBuilder>().spawn;
		largeSpawn = large.GetComponent<RoomBuilder>().spawn;
		//establish room to room pathing
		doors = new List<int>(new int[]{1,2,3});
		rooms = new List<int>(new int[]{1,2,3});
		createPaths(0,doors,rooms);
		createPaths(1,doors,rooms);
		createPaths(2,doors,rooms);

		complete = false;
		currentRoom = GetComponentInChildren<Playerrules>().currentRoom;
		reset = false;
		spawnEnemies = false;
		spawnItems = false;
		spawnTraps = false;

	}

	void Update () 
	{
		//this.currentRoom = GetComponentInChildren<Playerrules>().currentRoom;
		print("Rooms completed: " + roomsComplete);
		print("Total currency: " + totalCurrency);
		print("Enemies Defeated: " + enemiesDefeated);
		calcScore();
		print("Score: " + score);
		if (complete) 
		{
			levelsComplete++;
			print("Levels completed: " + levelsComplete);
			calcScore();
			print("Score: " + score);
			reset = true;
			Start();
		}
		if (reset) resetSpawns();
	}

	void createPaths(int path, List<int> doors, List<int> rooms)
	{
		int index, oneRoom, twoRoomA, twoRoomB;
		if (path == 0)
		{
			//pick a path door and remove from path list
			index = Random.Range(0,3);
			zeroPath = doors[index];
			this.doors.RemoveAt(index);

			zeroRoom = -2;

			//set spawns
			zeroSpawn = this.finalSpawn;
		}
		else if (path == 1)
		{
			//pick a path door and remove from path list
			index = Random.Range(0,2);
			onePath = doors[index];
			this.doors.RemoveAt(index);

			//set rooms
			index = Random.Range(0,3);
			oneRoom = rooms[index];
			this.rooms.RemoveAt(index);

			oneRooms[0] = oneRoom;
			oneRooms[1] = -2;

			//set spawns
			setSpawn(oneRoom,oneSpawns,0);
			finalExitRooms[0] = oneRoom;
			oneSpawns[1] = this.finalSpawn;
		}
		else if (path == 2)
		{
			//pick last remaining path door
			twoPath = doors[0];

			//set rooms
			index = Random.Range(0,2);
			twoRoomA = rooms[index];
			this.rooms.RemoveAt(index);
			twoRoomB = rooms[0];

			twoRooms[0] = twoRoomA;
			twoRooms[1] = twoRoomB;
			twoRooms[2] = -2;

			//set spawns
			setSpawn(twoRoomA,twoSpawns,0);
			setSpawn(twoRoomB,twoSpawns,1);
			finalExitRooms[1] = twoRoomB;
			twoSpawns[2] = this.finalSpawn;
		}
	}

	void setSpawn(int r, Vector3[] p, int index)
	{
		if (r == 1) p[index] = this.smallSpawn;
		if (r == 2) p[index] = this.mediumSpawn;
		if (r == 3) p[index] = this.largeSpawn;
	}

	void resetSpawns()
	{
		reset = false;
		spawnEnemies = true;
		spawnItems = true;
		spawnTraps = true;
	}

	void calcScore()
	{
		score = (levelsComplete*20)+(roomsComplete*5)+(enemiesDefeated*5)+(totalCurrency);
	}

}
