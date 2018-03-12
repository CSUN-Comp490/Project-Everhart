using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	public int currentRoom; //(final=-2,start=-1,small=1,medium=2,large=3)
	public string lastMove; //(right = 1, left = 2, up = 3, down = 4)

	void Start () 
	{
		//initialize player at the start room spawn (always)
		currentRoom = -1; 
		//transform.position = GetComponentInParent<GameManager> ().startSpawn;
		transform.position = new Vector3(20.8f,20.8f,-1f);
		GetComponentInParent<GameManager>().camera.transform.position = new Vector3(20.8f,-20.8f,-1f);
		GetComponentInParent<GameManager>().camera.orthographicSize = 25;
	}

	void Update () 
	{
		//player movement
		if (Input.GetKeyDown ("right")) move(3.2f,0f,"right");
		if (Input.GetKeyDown ("left")) move(-3.2f,0f,"left");
		if (Input.GetKeyDown ("up")) move(0f,3.2f,"up");
		if (Input.GetKeyDown ("down")) move(0f,-3.2f,"down");
	}

	//collisions
	void OnCollisionEnter2D(Collision2D other)
	{	
		if (other.gameObject.tag == "Hole")
		{

		}
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Hole" || other.gameObject.tag == "Lava")
		{
			if (lastMove == "right") move(-3.2f,0f,"left");
			else if (lastMove == "left") move(3.2f,0f,"right");
			else if (lastMove == "up") move(0f,-3.2f,"down");
			else if (lastMove == "down") move(0f,3.2f,"up");
		}
/* 

		//if player hits path1 door
		if (other.gameObject.tag == "Path1") 
		{
			//if this door is the zero path
			if(GetComponentInParent<GameManager>().zeroPath == 1)
			{
				transform.position = GetComponentInParent<GameManager> ().finalSpawn;
				GetComponentInParent<GameManager>().camera.transform.position = new Vector3(240f,-40f,-1f);
				GetComponentInParent<GameManager> ().camera.orthographicSize = 45;
				currentRoom = -2;
			}
			//if this door is the one path
			if(GetComponentInParent<GameManager>().zeroPath == 2)
			{
				
			}
			//if this door is the two path
			if(GetComponentInParent<GameManager>().zeroPath == 3)
			{

			}

		}
		if (other.gameObject.tag == "Path2") 
		{
			
		}
		if (other.gameObject.tag == "Path3") 
		{
			
		}
*/
		

		if (other.gameObject.tag == "Path1") 
		{
			transform.position = GetComponentInParent<GameManager> ().smallSpawn;
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(20.8f,79.2f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 25;
			currentRoom = 1;
		}
		if (other.gameObject.tag == "Path2") 
		{
			transform.position = GetComponentInParent<GameManager> ().mediumSpawn;
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(230.4f,69.6f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 35;
			currentRoom = 2;
		}
		if (other.gameObject.tag == "Path3") 
		{
			transform.position = GetComponentInParent<GameManager> ().largeSpawn;
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(440f,60f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 45;
			currentRoom = 3;

		}
		if (other.gameObject.tag == "Exit" || other.gameObject.tag == "FinalExit") 
		{
			transform.position = GetComponentInParent<GameManager> ().finalSpawn;
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(240f,-40f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 45;
			currentRoom = -2;
		}
	}

	//change player's position
	void move(float x, float y, string moveType)
	{
		Vector3 currentPosition = transform.position;
		this.transform.position = currentPosition + new Vector3 (x, y, 0f);
		lastMove = moveType;
	}

}
