using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 

public class Playerrules : MonoBehaviour 
{
	public int row;
	public int column;
	GameObject player;
	public Transform weaponSpawn;
	public GameObject weapon;
	GameObject currentWeapon;
	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector2 (row, column);
		currentWeapon = weapon;
	}
	

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("right"))
			move(1, 0);
		else if (Input.GetKeyDown("left"))
			move(-1, 0);
		else if (Input.GetKeyDown("up"))
			move(0, 1);
		else if (Input.GetKeyDown("down"))
			move(0, -1);

		//base attack 
		else if (Input.GetKeyDown("left ctrl"))
		{
			Instantiate(currentWeapon, weaponSpawn.position, weaponSpawn.rotation); 
		}
	}
	/*
	void attack(int x, int y)
	{
		

	}*/
	void move(int x, int y)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (x, y);
		transform.position = end;
			

	}
}
