using UnityEngine;
using System.Collections;

public class Weaponrules : MonoBehaviour {

	public int range;
	// Use this for initialization
	void Start () {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (0, range);
		transform.position = end;
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