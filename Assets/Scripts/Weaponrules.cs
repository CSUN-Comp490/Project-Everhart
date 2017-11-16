using UnityEngine;
using System.Collections;

public class Weaponrules : MonoBehaviour {

	public int range;
	// Use this for initialization
	void Start () {
				StartCoroutine (swing ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator swing()
	{
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
}