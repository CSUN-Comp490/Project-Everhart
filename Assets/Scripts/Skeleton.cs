using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour {

	public int row;
	public int column;
	GameObject skull;

    public GameObject loot;
    public Transform lootSpawn;
	// Use this for initialization
	void Start () {
		transform.position = new Vector2 (row, column);
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void move()
	{
		

	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Weapon") 
		{
			Destroy (gameObject);
            Instantiate(loot, lootSpawn.position, lootSpawn.rotation);
        }

	}
}
