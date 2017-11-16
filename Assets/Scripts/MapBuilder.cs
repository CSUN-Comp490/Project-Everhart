using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class MapBuilder : MonoBehaviour {

	public int columns = 3; 										//Number of columns in our game board.
	public int rows = 3;											//Number of rows in our game board.
	public GameObject[] floorTiles;									//Array of floor prefabs.
	
	private Transform mapholder;
	//A list of possible locations to place tiles.
	private List<Vector3> gridPositions = new List <Vector3> ();
	
	//Clears our list gridPositions and prepares it to generate a new board.
		void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 0; x < columns; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 0; y < rows; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}

		//Sets up the outer walls and floor (background) of the game board.
		void MapSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			mapholder = new GameObject ("Map").transform;
			
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = 0; x < columns; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = 0; y < rows; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					
					
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (mapholder);
				}
			}
		}
		


	void Start () 
	{
		//creates floors
		MapSetup();
		//Reset our list of gridpositions.
		InitialiseList ();
	}
	
	
}
