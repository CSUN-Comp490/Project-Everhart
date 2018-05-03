using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {
    public float lifetime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
		if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
