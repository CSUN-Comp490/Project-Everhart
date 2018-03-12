using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
room locations
start 0 0 0 
final 200 0 0 
small 0 100 0 
medium 200 100 0 
large 400 100 0 
 */

public class GameManager : MonoBehaviour 
{
	public int currentRoom;
	public Camera camera;
	public GameObject player, start, final, small, medium, large;
	public Vector3 startSpawn,finalSpawn,smallSpawn,mediumSpawn,largeSpawn;

	public int zeroPath, onePath, twoPath;
	public Vector3 zeroSpawn;
	public int zeroRoom;
	public Vector3[] oneSpawns = new Vector3[2];
	public int[] oneRooms = new int[2];
	public Vector3[] twoSpawns = new Vector3[3];
	public int[] twoRooms = new int[3];
	public int[] finalExitRooms = new int[2];
	public List<int> paths;
	public List<int> rooms;

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
		paths = new List<int>(new int[]{1,2,3});
		rooms = new List<int>(new int[]{1,2,3});
		createPaths(0,paths,rooms);
		createPaths(1,paths,rooms);
		createPaths(2,paths,rooms);
	}

	void Update () 
	{

	}

	void createPaths(int path, List<int> paths, List<int> rooms)
	{
		int index, oneRoom, twoRoomA, twoRoomB;
		if (path == 0)
		{
			//pick a path door and remove from path list
			index = Random.Range(0,2);
			zeroPath = paths[index];
			this.paths.RemoveAt(index);

			zeroRoom = -2;

			//set spawns
			zeroSpawn = this.finalSpawn;
		}
		else if (path == 1)
		{
			//pick a path door and remove from path list
			index = Random.Range(0,1);
			onePath = paths[index];
			this.paths.RemoveAt(index);

			//set rooms
			index = Random.Range(0,2);
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
			twoPath = paths[0];

			//set rooms
			index = Random.Range(0,1);
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
}
