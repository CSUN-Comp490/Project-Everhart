using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour 
{
	public GameObject start;
	public GameObject final;
	public GameObject smallRoom;
	public GameObject mediumRoom;
	public GameObject largeRoom;

	void Start () 
	{
		//build start room
		Instantiate(start,this.transform);
		//build final room
		Instantiate(final,this.transform);
		//build individual rooms
		Instantiate(smallRoom,this.transform);
		Instantiate(mediumRoom,this.transform);
		Instantiate(largeRoom,this.transform);
		//connect the rooms

	}

	void Update () 
	{
		if (Input.GetKeyDown("down")) 
		{
			Start();
		}
	}
}
