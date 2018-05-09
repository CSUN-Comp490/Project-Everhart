using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {


    public float speed;
    public GameObject player, flame;
    public Transform flamespawn;
    Vector3 lastPos;
    // Use this for initialization
    void Start ()
    {
        lastPos = GameObject.Find("player").transform.position + new Vector3(1.6f,-1.6f,0f);
            //new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //Debug.Log(lastPos);
	}
	
	// Update is called once per frame
	void Update () {
        
        this.transform.position = Vector3.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);
        if (this.transform.position == lastPos)
        {
            Destroy(gameObject);
            //Instantiate(flame, flamespawn.position, flamespawn.rotation);
            Instantiate(flame, (flamespawn.position + new Vector3(3.2f, 0f, 0f)), flamespawn.rotation);
            Instantiate(flame, (flamespawn.position + new Vector3(-3.2f, 0f, 0f)), flamespawn.rotation);
            Instantiate(flame, (flamespawn.position + new Vector3(0f, 3.2f, 0f)), flamespawn.rotation);
            Instantiate(flame, (flamespawn.position + new Vector3(0f, -3.2f, 0f)), flamespawn.rotation);

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
        /*
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        */
    }
}
