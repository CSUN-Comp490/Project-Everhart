﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skeleton : MonoBehaviour {

	GameObject skull;
    public GameObject player, loot;
    public List<GameObject> otherEnemies;
    public Transform lootSpawn;
    public int choice;
    public float moveRate, nextMove, time, moveX, moveY;
    public string lastMove;
    
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject child in GetComponentInParent<Playerrules>().transform)
        {
            if (child != this.gameObject) otherEnemies.Add(child);
        }
		moveRate = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.time;
        if(time > nextMove)
        {
            nextMove = time + moveRate;
            skeletonChoice();
        }
	}

    void move(float x, float y, string move)
	{
		lastMove = move;
		moveX = x;
		moveY = y;
		this.transform.position += new Vector3 (x, y, 0f);
	}

    bool playerNearby()
    {
        if (this.transform.position == (player.transform.position + new Vector3(3.2f,0f,0f))) return true;
        else if (this.transform.position == (player.transform.position + new Vector3(-3.2f,0f,0f))) return true;
        else if (this.transform.position == (player.transform.position + new Vector3(0f,3.2f,0f))) return true;
        else if (this.transform.position == (player.transform.position + new Vector3(0f,-3.2f,0f))) return true;
        return false;
    }

    bool skeletionNearby()
    {
        Vector3 a = this.transform.position + new Vector3(3.2f,0f,0f);
        Vector3 b = this.transform.position + new Vector3(-3.2f,0f,0f);
        Vector3 c = this.transform.position + new Vector3(0f,3.2f,0f);
        Vector3 d = this.transform.position + new Vector3(0f,-3.2f,0f);

        foreach (GameObject skel in otherEnemies)
        {
            if (skel.transform.position == a) return false;
            if (skel.transform.position == b) return false;
            if (skel.transform.position == c) return false;
            if (skel.transform.position == d) return false;
        }
        return false;
    }

    void attackPlayer() //for now it just moves into player, which will hurt player
    {
        Vector3 startPos = this.transform.position;
        this.transform.position = GetComponent<Playerrules>().transform.position;
        this.transform.position = startPos;
    }

    void skeletonChoice()
    {
        if (!playerNearby()) //chance to attack or move
        {
            choice = Random.Range(0,4);
            if (choice == 0) move(0f,3.2f,"up");
            else if (choice == 1) move(0f,-3.2f,"down");
            else if (choice == 2) move(3.2f,0f,"right");
            else if (choice == 3) move(-3.2f,0f,"left");
        }
        else //just move
        {
            choice = Random.Range(0,6);
            if (choice == 0) move(0f,3.2f,"up");
            else if (choice == 1) move(0f,-3.2f,"down");
            else if (choice == 2) move(3.2f,0f,"right");
            else if (choice == 3) move(-3.2f,0f,"left");
            else if (choice >= 4) attackPlayer();
        }  
    }

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Weapon") 
		{
			Destroy (gameObject);
            Instantiate(loot, lootSpawn.position, lootSpawn.rotation);
        }
        if (other.gameObject.tag == "Wall")
		{
			moveX = -moveX;
			moveY = -moveY;
			move(moveX, moveY,lastMove);
		}
	}
}
