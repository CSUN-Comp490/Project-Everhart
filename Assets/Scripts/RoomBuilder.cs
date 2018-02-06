using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {

	public GameObject walls;
	public GameObject entrance;
	public GameObject exit;
	public GameObject finalExit;
	public GameObject[] floorPrefabs = new GameObject[4];
	//public GameObject[] actualTiles = new GameObject[4];
	public int[,] floorTiles;
	public float size;
	public float squareLength = 3.2f;
	public float wallWidth = 6.4f;
	public float tileWidth = 9.6f;
	public bool isFinalExit;

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
		Instantiate(walls,new Vector3((size-1f)*100f,100f,0f),Quaternion.identity,this.transform);

		//place exit
		isFinalExit = false;
		int a = Random.Range(0,(int)size*2);
		Vector3 exitPosition = new Vector3 (((size-1f)*100f) + wallWidth + (a*tileWidth),100f - wallWidth,0f);
		if (!isFinalExit) Instantiate(exit,exitPosition,Quaternion.identity,this.transform);
		else Instantiate(finalExit,exitPosition,Quaternion.identity,this.transform);
		floorTiles[0,a] = 1; 

		//place entrance
		int b = Random.Range(0,(int)size*2);
		Vector3 entrancePosition = new Vector3(((size-1f)*100f) + (wallWidth) + (b*tileWidth),
			100f - (wallWidth) - ((size*2f)*(tileWidth)),0f);
		Instantiate(entrance,entrancePosition,Quaternion.identity,this.transform);
		floorTiles[((int)size*2),b] = 1;

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
						Quaternion.identity,this.transform);
					floorTiles[i,j] = 1;
				}
			}
		}

	}

	void Update () 
	{
				
	}
}
