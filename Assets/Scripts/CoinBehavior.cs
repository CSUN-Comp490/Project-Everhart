using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{

			Destroy (gameObject);
		}

	}
}
