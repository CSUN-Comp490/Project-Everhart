using UnityEngine;
using System.Collections;

public class potpower : MonoBehaviour {

	public int row, column;
	// Use this for initialization
	void Start () {
		//transform.position = new Vector2 (row, column);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
		}

	}
}
