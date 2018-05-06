using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss1 : MonoBehaviour
{

    GameObject boss;
    public GameObject player, loot1, loot2, loot3, loot4, loot5, loot6, fireball;
    public Transform lootSpawn;
    public Transform fireballSpawn;
    public Vector3 spawn;
    private bool active, attacking;
    public int hp;
    public float moveRate, attackRate, nextMove, nextAttack;
    private float time, moveX, moveY;
    Animator anim;
    


    // Use this for initialization
    void Start()
    {
        active = false;
        attacking = false;
        //used to reference player position
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        spawn = this.transform.position;

        hp = 1 + GetComponentInParent<GameManager>().levelsComplete;

    /* 
        if (GetComponentInParent<GameManager>().levelsComplete > 0)
        {
            hp = GetComponentInParent<GameManager>().levelsComplete;
        }
        else hp = 1;
        */
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        //movement method
        if (time > nextMove && active == true) 
        {
            nextMove = time + moveRate;
            bossChoice();
        }
        //attack method
        if (time > nextAttack && active == true)
        {
            //attacking = true;
            nextAttack = time + attackRate;
            attackPlayer();
        }

        ResetValues();//ignore
        checkHP();
    }

    //moves boss to new position.
    void move(float x, float y)
    {
        this.transform.position += new Vector3(x, y, 0f);
    }

    //will do an attack sequence then fire a fire ball.
    void attackPlayer() 
    {
        //instantiate fire ball after a small period of time
        anim.Play("BossAttack");
        Instantiate(fireball, fireballSpawn.position, fireballSpawn.rotation);
        
    }

    void bossChoice()
    {
        //find the distance between two points: x's and y's
        moveX = Mathf.Abs(player.transform.position.x - this.transform.position.x);
        moveY = Mathf.Abs(player.transform.position.y - this.transform.position.y);
        
        //this section finds which distance is greater.
        if (moveX > moveY && (this.transform.position.x != player.transform.position.x))
        {
            if((player.transform.position.x - this.transform.position.x) < 0)
            {
                move(-3.2f, 0f);
            }
            else
            {
                move(3.2f, 0f);
            }
        }
        else
        {
            //moveY being negative indicates that 
            if((player.transform.position.y - this.transform.position.y) < 0)
            {
                move(0f, -3.2f);
            }
            else
            {
                move(0f, 3.2f);
            }
        }
    }

    void damage()
    {
        hp--;
    }

    void checkHP()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
            GetComponentInParent<GameManager>().enemiesDefeated++;
            GetComponentInParent<GameManager>().bossDead = true;
            Transform trans = GetComponentInParent<EnemyManager>().transform;
            Instantiate(loot1, spawn + new Vector3(1.6f,1.6f,0f), lootSpawn.rotation, trans);
            Instantiate(loot2, spawn + new Vector3(-4.8f,1.6f,0f), lootSpawn.rotation, trans);
            Instantiate(loot3, spawn + new Vector3(-4.8f,-1.6f,0f), lootSpawn.rotation, trans);
            Instantiate(loot4, spawn + new Vector3(-1.6f,1.6f,0f), lootSpawn.rotation, trans);
            Instantiate(loot5, spawn + new Vector3(-1.6f,-1.6f,0f), lootSpawn.rotation, trans);
            Instantiate(loot6, spawn + new Vector3(1.6f,-1.6f,0f), lootSpawn.rotation, trans);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = true;
            Debug.Log("entered range");
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.tag == "Weapon")
        {
            damage();
        }
       
    }
    private void ResetValues()
    {
        attacking = false;

    }
}
