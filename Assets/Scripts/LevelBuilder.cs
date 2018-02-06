using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour 
{
	const int small = 1;
	const int medium = 2;
	const int large = 3;
	public GameObject startRoom;
	public GameObject finalRoomBoss;
	public GameObject smallRoom;
	public GameObject mediumRoom;
	public GameObject largeRoom;

	void Start () 
	{
		//build start room
		Instantiate(startRoom,new Vector3(0,0,0),Quaternion.identity,this.transform);
		//build final room
		Instantiate(finalRoomBoss,new Vector3(100,0,0),Quaternion.identity,this.transform);
		//build individual rooms
		Instantiate(smallRoom,this.transform);
		Instantiate(mediumRoom,this.transform);
		Instantiate(largeRoom,this.transform);
		//connect the rooms
	}

	void Update () 
	{
		
	}
}
