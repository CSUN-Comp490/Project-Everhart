using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTester : MonoBehaviour {

	public bool inCollider;
	// Use this for initialization
	void Start () 
	{
		inCollider = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Hole")
		{
			inCollider = true;
		}
	}
}
