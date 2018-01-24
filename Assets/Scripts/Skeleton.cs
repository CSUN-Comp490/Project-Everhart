using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour {

	
    public float moveRate = 0.5f;
    public float nextMove = 0.0f;
	GameObject skull;

    private bool tool = false;
    public int patrolx;
    public int patroly;
    public int countx = 0;
    public int county = 0;

    Vector2 start, end;

    

    public GameObject loot;
    public Transform lootSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time > nextMove)
        {
            nextMove = Time.time + moveRate;
            patrol();

        }
		
	
	}

    void move(int x, int y)
    {
       
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(x, y);
        transform.position = end;
     
    }

    void patrol()
	{
        if(tool!= true)
        {
            if (countx < patrolx)
            {
                move(1, 0);
                countx++;
            }
            else
            {
                move(0, -1);
                county++;

            }
            if (countx == patrolx && county == patroly)
            {
                tool = true;
            }
        }
        else
        {
            if (countx > 0)
            {
                move(-1, 0);
                countx--;
            }
            else
            {
                move(0, 1);
                county--;

            }
            if (countx == 0 && county == 0)
            {
                tool = false;
            }

        }
        

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
