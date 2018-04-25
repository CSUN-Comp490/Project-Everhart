using UnityEngine;
using System.Collections;

public class potpower : MonoBehaviour {

	void Start () 
	{	
	}
	
	void Update () 
	{
	
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<Playerrules>().curHp 
				!= other.gameObject.GetComponent<Playerrules>().maxHeartsAmount*2)
			{
				Destroy (gameObject);
			}
			
		}

	}
}
