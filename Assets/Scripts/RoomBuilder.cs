using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {

	public GameObject startPrefab, finalPrefab, walls, entrance, exit, finalExit;
	public GameObject[] floorPrefabs = new GameObject[4];
	public int[,] floorTiles;
	public float size; //(final=-2,start=-1,small=1,medium=2,large=3)
	public int orientation, a, b; //variables for entrance/exit placement
	public Vector3 entrancePosition, exitPosition;
	public float squareLength = 3.2f; //width/length of one square
	public float wallWidth = 6.4f; //width of the wall prefabs
	public float tileWidth = 9.6f; //width of one floor tile prefab (3 squares x 3 squares)
	public bool isFinalExit;

	 /*for reference - corner positions
	 Vector3 upperLeft = new Vector3 (((size - 1f) * 100f) + (wallWidth), 100f - (wallWidth), 0f);
	 Vector3 upperRight = new Vector3 (((size - 1f) * 100f + (wallWidth) + ((size * 2f) * (tileWidth))),
	 100f - (wallWidth), 0f);
	 Vector3 bottomRight = new Vector3 (((size - 1f) * 100f + (wallWidth) + ((size * 2f) * (tileWidth))),
	 100f - (wallWidth) - ((size * 2f) * (tileWidth)), 0f);
	 Vector3 bottomLeft = new Vector3 (((size - 1f) * 100f) + (wallWidth),
	 100f - (wallWidth) - ((size * 2f) * (tileWidth)), 0f);
	 */ 

	void Start () {
		//start room
		if (size == -1) 
		{
			Instantiate(startPrefab,new Vector3(0f,0f,0f),Quaternion.identity,this.transform);
		}
		//final room
		else if (size == -2) 
		{
			Instantiate(finalPrefab,new Vector3(100f,0f,0f),Quaternion.identity,this.transform);
		}
		//small/med/large rooms
		else 
		{	
			//initialize array of floorTiles to 0 (empty)
			floorTiles = new int[(((int)size * 2) + 1), (((int)size * 2) + 1)];
			for (int i = 0; i < (((int)size * 2) + 1); i++) {
				for (int j = 0; j < (((int)size * 2) + 1); j++) {
					floorTiles [i, j] = 0;
				}
			}

			//place wall prefab
			Instantiate (walls, new Vector3 ((size - 1f) * 100f, 100f, 0f), Quaternion.identity, this.transform);

			//determine orientation of entrance/exits (0 = upper/lower rows, 1 = left/right columns) 
			orientation = Random.Range(0,2);
			if (orientation == 0) 
			{
				//place exit
				isFinalExit = false;
				a = Random.Range (0, (int)size * 2);
				exitPosition = new Vector3 (((size - 1f) * 100f) + wallWidth + (a * tileWidth), 100f - wallWidth, 0f);
				if (!isFinalExit)
					Instantiate (exit, exitPosition, Quaternion.identity, this.transform);
				else
					Instantiate (finalExit, exitPosition, Quaternion.identity, this.transform);
				floorTiles [0,a] = 1; 

				//place entrance
				b = Random.Range (0, (int)size * 2);
				entrancePosition = new Vector3 (((size - 1f) * 100f) + (wallWidth) + (b * tileWidth),
					100f - (wallWidth) - ((size * 2f) * (tileWidth)), 0f);
				Instantiate (entrance, entrancePosition, Quaternion.identity, this.transform);
				floorTiles [((int)size * 2),b] = 1;
			} 
			else if (orientation == 1) 
			{
				//place exit
				isFinalExit = false;
				a = Random.Range (0, (int)size * 2);
				exitPosition = new Vector3 (((size - 1f) * 100f) + wallWidth + ((size * 2f)*(tileWidth)), 100f - wallWidth - (a * tileWidth), 0f);
				if (!isFinalExit)
					Instantiate (exit, exitPosition, Quaternion.identity, this.transform);
				else
					Instantiate (finalExit, exitPosition, Quaternion.identity, this.transform);
				floorTiles [a,((int)size * 2)] = 1; 

				//place entrance
				b = Random.Range (0, (int)size * 2);
				entrancePosition = new Vector3 (((size - 1f) * 100f) + (wallWidth),
					100f - (wallWidth) - (b * tileWidth), 0f);
				Instantiate (entrance, entrancePosition, Quaternion.identity, this.transform);
				floorTiles [b,0] = 1;
			}

			//for each remaining index spot place a random floor prefab
			for (int i = 0; i < (((int)size * 2) + 1); i++) {
				for (int j = 0; j < (((int)size * 2) + 1); j++) {
					if (floorTiles [i, j] == 0) {
						GameObject floorPrefab = floorPrefabs [Random.Range (0, floorPrefabs.Length)]; 
						//pick random floor prefab from array
						GameObject randomFloorTile = Instantiate (floorPrefab,
							                             new Vector3 (((size - 1f) * 100f) + (wallWidth) + (j * tileWidth), 100f - (wallWidth) - (i * tileWidth), 0f),
							                             Quaternion.identity, this.transform);
						floorTiles [i, j] = 1;
					}
				}
			}
		}
	}

	void Update () 
	{
		if(Input.GetKeyDown("down"))
		{
			Destroy(gameObject);
		}
	}
}
