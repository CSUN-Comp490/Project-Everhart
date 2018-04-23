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
	private int maxHeartsAmount = 6;
	private int maxHp;
	private int healthPerHeart = 2;
	private float moveX, moveY;
	public Image[] healthImages;
	public Sprite[] healthSprites;
	public GameObject[] enemiesTest;
	public GameObject[] itemsTest;
	public GameObject useSprite;
	public GameObject coinsTest;
	int dice;

	public int currency = 0;

	public GameObject useItem;
	public GameObject tomb;

	Animator anim;

	public GameObject weapon;
	public Transform weaponSpawn;
	GameObject currentWeapon;
	private bool attacking = false;
	public float atkRate;
	public float nextAtk;

	public int currentRoom; //(final=-2,start=-1,small=1,medium=2,large=3)
	public int roomNum;
	public int currentPath; //0, 1, or 2
	public string lastMove; //(right = 1, left = 2, up = 3, down = 4)

	// Use this for initialization
	void Start()
	{
		currentRoom = -1;
		transform.position = GetComponentInParent<GameManager>().startSpawn;
		//resetCamera();

		currentWeapon = weapon;
		weaponSpawn.transform.position = this.transform.position + new Vector3(1.6f,1.6f,0f);

		curHp = startHearts * healthPerHeart;
		maxHp = maxHeartsAmount * healthPerHeart;
		checkHp();
		anim = GetComponent<Animator>();
	}


	// Update is called once per frame
	void Update()
	{
		if (useItem != null) useSprite.GetComponent<Image>().sprite = useItem.GetComponent<SpriteRenderer>().sprite;

		if (!attacking)
		{
			if (Input.GetKeyDown("right"))
			{
				weaponSpawn.GetComponent<Transform>().eulerAngles = new Vector3 (0,0,-90);
				weaponSpawn.transform.position = this.transform.position + new Vector3(4.8f,-1.6f,0f);
				move(3.2f, 0f,"right");
				anim.Play("right");
			}
			else if (Input.GetKeyDown("left"))
			{
				weaponSpawn.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);
				weaponSpawn.transform.position = this.transform.position + new Vector3(-1.6f,-1.6f,0f);
				move(-3.2f, 0f,"left");
				anim.Play("left");
			}
			else if (Input.GetKeyDown("up"))
			{
				weaponSpawn.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
				weaponSpawn.transform.position = this.transform.position + new Vector3(1.6f,1.6f,0f);
				move(0f, 3.2f,"up");
				anim.Play("up");
			}
			else if (Input.GetKeyDown("down"))
			{
				weaponSpawn.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
				weaponSpawn.transform.position = this.transform.position + new Vector3(1.6f,-4.8f,0f);
				move(0f, -3.2f,"down");
				anim.Play("down");
			}

		}

		//use usable item
		if (Input.GetKeyDown ("left shift")) 
		{
			Instantiate (useItem, weaponSpawn.position, weaponSpawn.rotation);
		}
		if (Input.GetKeyDown ("j")) 
		{
			PlayerPrefs.SetInt (enemiesTest [0].name, 1);
		}
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

	void move(float x, float y, string move)
	{
		lastMove = move;
		moveX = x;
		moveY = y;
		this.transform.position += new Vector3 (x, y, 0f);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Monster")
		{
			Damage(-1);
			moveX = -moveX;
			moveY = -moveY;
			move(moveX,moveY,lastMove);
		}
		if (other.gameObject.tag == "Coin")
		{
			GetComponentInParent<GameManager>().totalCurrency++;
            ScoreManager.score += 1;
		}
		if(other.gameObject.tag == "Trap")
		{
			Damage(-1);
		}
		if (other.gameObject.tag == "Healing Pot")
		{
			//inc player health
		}
		if (other.gameObject.tag == "Hole" || other.gameObject.tag == "Lava")
		{
			Damage(-1);
			moveX = -moveX;
			moveY = -moveY;
			move(moveX, moveY,lastMove);
		}
		if (other.gameObject.tag == "Wall")
		{
			moveX = -moveX;
			moveY = -moveY;
			move(moveX, moveY,lastMove);
		}

		//if player hits path1 door
		if (other.gameObject.tag == "Path1") 
		{
			if(GetComponentInParent<GameManager>().zeroPath == 1) sendToZeroPath();
			if(GetComponentInParent<GameManager>().onePath == 1) sendToOnePath();
			if(GetComponentInParent<GameManager>().twoPath == 1) sendToTwoPath();

		}
		if (other.gameObject.tag == "Path2") 
		{
			if(GetComponentInParent<GameManager>().zeroPath == 2) sendToZeroPath();
			if(GetComponentInParent<GameManager>().onePath == 2) sendToOnePath();
			if(GetComponentInParent<GameManager>().twoPath == 2) sendToTwoPath();
		}
		if (other.gameObject.tag == "Path3") 
		{
			if(GetComponentInParent<GameManager>().zeroPath == 3) sendToZeroPath();
			if(GetComponentInParent<GameManager>().onePath == 3) sendToOnePath();
			if(GetComponentInParent<GameManager>().twoPath == 3) sendToTwoPath();
		}
		if (other.gameObject.tag == "Exit") 
		{
			//send player back to start after final room
			if (currentRoom == -2) 
			{
				roomNum = 0;
				currentRoom = -1;
				currentPath = 0;
				GetComponentInParent<GameManager>().roomsComplete++;
				transform.position = GetComponentInParent<GameManager>().startSpawn;
				//resetCamera();
				GetComponentInParent<GameManager>().complete = true;
			}
			//send player to next room of onePath
			if (currentPath == 1)
			{
				roomNum++;
				GetComponentInParent<GameManager>().roomsComplete++;
				currentRoom = GetComponentInParent<GameManager>().oneRooms[roomNum];
				GetComponentInParent<GameManager>().currentRoom = currentRoom;

				spawnTIE();

				transform.position = GetComponentInParent<GameManager>().oneSpawns[roomNum];
				//resetCamera();
			}
			//send player to next room of twoPath
			if (currentPath == 2)
			{
				roomNum++;
				GetComponentInParent<GameManager>().roomsComplete++;
				currentRoom = GetComponentInParent<GameManager>().twoRooms[roomNum];
				GetComponentInParent<GameManager>().currentRoom = currentRoom;

				spawnTIE();

				transform.position = GetComponentInParent<GameManager>().twoSpawns[roomNum];
				//resetCamera();
			}
		}
	}

	void sendToZeroPath()
	{
		//set to correct path
		currentPath = 0;
		//set to first room in that path
		currentRoom = GetComponentInParent<GameManager>().zeroRoom;
		GetComponentInParent<GameManager>().currentRoom = currentRoom;

		spawnTIE();

		//move player
		transform.position = GetComponentInParent<GameManager>().zeroSpawn;
		//move camera (temp)
		//resetCamera();
	}

	void sendToOnePath()
	{
		roomNum = 0;
		//set to correct path
		currentPath = 1;
		//set to first room in that path
		currentRoom = GetComponentInParent<GameManager>().oneRooms[roomNum];
		GetComponentInParent<GameManager>().currentRoom = currentRoom;

		spawnTIE();
		
		//move player
		transform.position = GetComponentInParent<GameManager>().oneSpawns[roomNum];
		//move camera (temp)
		//resetCamera();
	}

	void sendToTwoPath()
	{
		roomNum = 0;
		//set to correct path
		currentPath = 2;
		//set to first room in that path
		currentRoom = GetComponentInParent<GameManager>().twoRooms[roomNum];
		GetComponentInParent<GameManager>().currentRoom = currentRoom;

		spawnTIE();

		//move player
		transform.position = GetComponentInParent<GameManager>().twoSpawns[roomNum];
		//move camera (temp)
		//resetCamera();
	}

	void resetCamera()
	{
		if (currentRoom == 1)
		{
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(20.8f,79.2f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 25;
		}
		if (currentRoom == 2)
		{
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(230.4f,69.6f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 35;
		}
		if (currentRoom == 3)
		{
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(440f,60f,-1f);
			GetComponentInParent<GameManager>().camera.orthographicSize = 45;
		}
		if (currentRoom == -1)
		{
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(20.8f,-20.8f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 25;
		}
		if (currentRoom == -2)
		{
			GetComponentInParent<GameManager>().camera.transform.position = new Vector3(240f,-40f,-1f);
			GetComponentInParent<GameManager> ().camera.orthographicSize = 45;
		}
	}

	void spawnTIE()
	{
		GetComponentInParent<GameManager>().reset = true;
		GetComponentInParent<GameManager>().spawnEnemies = true;
		GetComponentInParent<GameManager>().spawnItems = true;
		GetComponentInParent<GameManager>().spawnTraps = true;
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

		PlayerPrefs.GetInt("Score",0);
		PlayerPrefs.SetInt("Score", GetComponentInParent<GameManager>().score);
		currency = PlayerPrefs.GetInt("Coins",0);
		currency += GetComponentInParent<GameManager>().totalCurrency;
		PlayerPrefs.SetInt("Coins", currency);

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
