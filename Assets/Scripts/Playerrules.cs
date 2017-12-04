using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 

public class Playerrules : MonoBehaviour 
{
	public int row;
	public int column;
    public bool attackStatus = true;
    public bool moveStatus = true;
    GameObject player;
	public Transform weaponSpawn;
	public GameObject weapon;
	GameObject currentWeapon;
    private Animator anim;
    public GameObject d_sword;
    public GameObject l_sword;
    public GameObject r_sword;



    public GameObject[] swordDirections;
    /*
    [0] = up
    [1] = down  
    [2] = left 
    [3] = right
     */
    

    
	// Use this for initialization
	void Start () 
	{

        anim = GetComponent<Animator>();

        transform.position = new Vector2 (row, column);
        
		currentWeapon = d_sword;
	}


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("right"))
        {

            if (moveStatus == true)
            {
                row = 1;
                column = 0;

                anim.SetInteger("xAxis", row);
                anim.SetInteger("YAxis", column);

                move(row, column);
            }
        }

        else if (Input.GetKeyDown("left"))
        {
            if (moveStatus == true)
            {
                row = -1;
                column = 0;

                anim.SetInteger("xAxis", row);
                anim.SetInteger("YAxis", column);

                move(row, column);

            }
        }

        else if (Input.GetKeyDown("up"))
        {
            if (moveStatus == true)
            {
                row = 0;
                column = 1;


                anim.SetInteger("xAxis", row);
                anim.SetInteger("YAxis", column);

                move(row, column);
            }
        }

        else if (Input.GetKeyDown("down"))
        {
            if (moveStatus == true)
            {
                row = 0;
                column = -1;

                anim.SetInteger("xAxis", row);
                anim.SetInteger("YAxis", column);

                move(row, column);

            }

        }


        //standard attacks

        else if (Input.GetKeyDown("w")) //attack foward
        {

          
                row = 0;
                column = 1;

                anim.SetInteger("xAxis", row);
                anim.SetInteger("YAxis", column);
                currentWeapon = swordDirections[0];
                attack(0, 0);
            
        }
        else if (Input.GetKeyDown("s")) //attack down
        {
            row = 0;
            column = -1;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[1];

            attack(0, -2);
        }

        else if (Input.GetKeyDown("a")) //atttack left
        {
            row = -1;
            column = 0;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[2];

            attack(-1, -1);
        }

        else if (Input.GetKeyDown("d")) //attack right
        {

            row = 1;
            column = 0;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[3];

            attack(1, -1);
        }


        //range attacks below

        else if (Input.GetKeyDown("1"))//attack up
        {

            row = 0;
            column = 1;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[0];
            attack(0, 1);
        }
        else if (Input.GetKeyDown("2")) //attack left
        {

            row = -1;
            column = 0;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[2];

            attack(-2, -1);

        }

        else if (Input.GetKeyDown("3")) //attack right
        {

            row = 1;
            column = 0;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[3];
            attack(2, -1);

        }

        else if (Input.GetKeyDown("4")) //attack down
        {

            row = 0;
            column = -1;

            anim.SetInteger("xAxis", row);
            anim.SetInteger("YAxis", column);
            currentWeapon = swordDirections[1];
            attack(0, -3);
        }

       
    }
    
	void attack(int x, int y)
	{

        if (attackStatus == true)
        {

            attackStatus = false;
            moveStatus = false;
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(x, y);
            weaponSpawn.position = end;
            Instantiate(currentWeapon, weaponSpawn.position, weaponSpawn.rotation);

            StartCoroutine("HoldOn");

        }
       
    }


    void move(int x, int y)
	{

        if (moveStatus == true)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(x, y);
            transform.position = end;
        }
	}

    IEnumerator HoldOn()
    {

        yield return new WaitForSeconds(0.3f);
        print("attack");
        attackStatus = true;
        moveStatus = true;

    }





}
