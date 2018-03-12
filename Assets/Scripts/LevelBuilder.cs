using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour 
{
	public GameObject start,final,small,medium,large;
	public Vector3 startSpawn,finalSpawn,smallSpawn,mediumSpawn,largeSpawn;

	void Start () 
	{
		//build start room
		//Instantiate(start,new Vector3(0f,0f,0f),Quaternion.identity,this.transform);
		//build final room
		//Instantiate(final,new Vector3(200f,0f,0f),Quaternion.identity,this.transform);
		//build individual rooms
		//Instantiate(smallRoom,this.transform);
		//Instantiate(mediumRoom,this.transform);
		//Instantiate(largeRoom,this.transform);

		startSpawn = start.GetComponent<RoomBuilder>().spawn;
		finalSpawn = final.GetComponent<RoomBuilder>().spawn;
		smallSpawn = small.GetComponent<RoomBuilder>().spawn;
		mediumSpawn = medium.GetComponent<RoomBuilder>().spawn;
		largeSpawn = large.GetComponent<RoomBuilder>().spawn;
	}

	void Update () 
	{
		if (Input.GetKeyDown("space")) 
		{
			Start();
		}
	}
}
