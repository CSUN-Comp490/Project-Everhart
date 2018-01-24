using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Playerrules : MonoBehaviour
{
    //health stuff
    public int curHp;
    public int startHearts = 1;
    private int maxHeartsAmount = 7;
    private int maxHp;
    private int healthPerHeart = 2;
    private int moveX, moveY;
    public Image[] healthImages;
    public Sprite[] healthSprites;

    public int row;
    public int column;
    GameObject player;
    public int currency = 0;
    public Transform weaponSpawn;
    public GameObject weapon;
    public GameObject tomb;
    GameObject currentWeapon;
    public Transform playerSpawn;
    private bool attacking = false;

    Animator anim;

    public float atkRate;
    public float nextAtk;

    // Use this for initialization
    void Start()
    {
        transform.position = playerSpawn.position;
        currentWeapon = weapon;
        curHp = startHearts * healthPerHeart;
        maxHp = maxHeartsAmount * healthPerHeart;
        checkHp();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (Input.GetKeyDown("right"))
                attemptmove(1, 0);
            
            else if (Input.GetKeyDown("left"))
                attemptmove(-1, 0);

            else if (Input.GetKeyDown("up"))
                attemptmove(0, 1);

            else if (Input.GetKeyDown("down"))
                attemptmove(0, -1);
        }
        //base attack 
        /*
		 * if w set spawn to a position relative to player position
		 * a...moves spawn and rotates sprite
		 * s...
		 * d...w

	*/

        if (Input.GetKeyDown("left ctrl") && Time.time > nextAtk)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        attacking = true;
        float timer = 0.0f;
        Instantiate(currentWeapon, weaponSpawn.position, weaponSpawn.rotation);
        while (timer < atkRate)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        attacking = false;

    }

    void attemptmove (int x, int y)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(x, y);
        weaponSpawn.transform.position = end;
        moveX = x;
        moveY = y;
        move(x, y);
        



    }
    void move(int x, int y)
    {
        if (x == 1 && y == 0)
            anim.SetTrigger("moveRight");
        if (x == -1 && y == 0)
            anim.SetTrigger("moveLeft");

        if (x == 0 && y == -1)
            anim.SetTrigger("moveDown");
        if (x == 0 && y == 1)
            anim.SetTrigger("moveUp");

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(x, y);
        transform.position = end;


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Damage(-1);
            moveX = -moveX;
            moveY = -moveY;
            move(moveX, moveY);
        }

        if (other.gameObject.tag == "Coin")
        {
             ScoreManager.score += 1;
        }

        if(other.gameObject.tag == "Trap")
        {
            Damage(-1);
        }

        if (other.gameObject.tag == "Pickups")
        {
            Damage(1);
        }
        if (other.gameObject.tag == "Final Exit")
        {
            SceneManager.LoadScene("GameOver");
        }
        if (other.gameObject.tag == "Hole")
        {
            Damage(-1);
            moveX = -moveX;
            moveY = -moveY;
            move(moveX, moveY);
        }
		/*
		if (other.gameObject.tag == "Wall")
		{
			moveX = -moveX;
			moveY = -moveY;
			move(moveX, moveY);
		}
		*/
    }

    void checkHp()
    {
        for (int i = 0; i < maxHeartsAmount; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHp();
    }

    void UpdateHp()
    {
        bool empty = false;
        int i = 0;

        //checks for each image
        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (curHp >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHP = (int)(healthPerHeart - (healthPerHeart * i - curHp));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHP / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;

                }
            }
        }
    }

    public void Damage(int amount)
    {
        curHp += amount;
        curHp = Mathf.Clamp(curHp, 0, startHearts * healthPerHeart);
        UpdateHp();
        if(curHp == 0)
        {
            
            gameOver();
        }
    }

    void gameOver()
    {
        Destroy(gameObject);
        Instantiate(tomb, transform.position, transform.rotation);
        SceneManager.LoadScene("GameOver");
    }

    public void AddHeartContainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartsAmount);

        //curHp = startHearts * healthPerHeart;
        //maxHp = maxHeartsAmount * healthPerHeart;

        checkHp();
    }


}
