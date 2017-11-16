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
    public Image[] healthImages;
    public Sprite[] healthSprites;

    public int row;
    public int column;
    GameObject player;
    public int currency = 0;
    public Transform weaponSpawn;
    public GameObject weapon;
    GameObject currentWeapon;
    public Transform playerSpawn;
    private bool attacking = false;



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
    }


    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (Input.GetKeyDown("right"))
                move(1, 0);

            else if (Input.GetKeyDown("left"))
                move(-1, 0);

            else if (Input.GetKeyDown("up"))
                move(0, 1);

            else if (Input.GetKeyDown("down"))
                move(0, -1);
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


    void move(int x, int y)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(x, y);
        transform.position = end;


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Damage(-curHp);
           // Destroy(gameObject);
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
            //Destroy(gameObject);
            gameOver();
        }
    }

    void gameOver()
    {
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
