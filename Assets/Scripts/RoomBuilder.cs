using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {

	public Transform walls;
	public Transform entrance;
	public Transform exit;
	public Transform finalExit;
	public GameObject[] floorPrefabs = new GameObject[4];
	public int[,] floorTiles;
	public float size;
	public float squareLength = 3.2f;
	public float wallWidth = 6.4f;
	public float tileWidth = 9.6f;
	public bool isFinalExit;

	//add function to delete all instances to "reset" the world

	void Start () {
		//initialize array of floorTiles to 0 (empty)
		floorTiles = new int[(((int)size*2)+1),(((int)size*2)+1)];
		for (int i = 0; i < (((int)size*2)+1); i++)
		{
			for(int j = 0; j < (((int)size*2)+1); j++)
			{
				floorTiles[i,j] = 0;
			}
		}
		//corner positions
		Vector3 upperLeft = new Vector3(((size-1f)*100f) + (wallWidth),100f - (wallWidth),0f);
		Vector3 upperRight = new Vector3(((size-1f)*100f + (wallWidth) + ((size*2f)*(tileWidth))),
			100f - (wallWidth),0f);
		Vector3 bottomRight = new Vector3(((size-1f)*100f + (wallWidth) + ((size*2f)*(tileWidth))),
			100f - (wallWidth) - ((size*2f)*(tileWidth)),0f);
		Vector3 bottomLeft = new Vector3(((size-1f)*100f) + (wallWidth),
			100f - (wallWidth) - ((size*2f)*(tileWidth)),0f);

		//place wall prefab
		Instantiate(walls,new Vector3((size-1f)*100f,100f,0f),Quaternion.identity);

		//place exit
		isFinalExit = false;
		if (!isFinalExit) Instantiate(exit,upperRight,Quaternion.identity);
		else Instantiate(finalExit,upperRight,Quaternion.identity);
		floorTiles[0,((int)size*2)] = 1; //temp set upperRight spot to 1 (occupied)

		//place entrance
		Instantiate(entrance,bottomLeft,Quaternion.identity);
		floorTiles[((int)size*2),0] = 1; //temp set bottomLeft spot to 1 (occupied)

		//for each remaining index spot place a random prefab
		for(int i = 0; i < (((int)size*2)+1); i++)
		{
			for(int j = 0; j < (((int)size*2)+1); j++)
			{
				if(floorTiles[i,j] == 0)
				{
					GameObject floorPrefab = floorPrefabs[Random.Range(0,floorPrefabs.Length)]; 
					//pick random floor prefab from array
					GameObject randomFloorTile = Instantiate(floorPrefab,
						new Vector3(((size-1f)*100f)+(wallWidth)+(j*tileWidth), 100f-(wallWidth)-(i*tileWidth), 0f),
						Quaternion.identity) as GameObject;
					floorTiles[i,j] = 1;
				}
			}
		}

	}

	void Update () 
	{
		
	}
}
