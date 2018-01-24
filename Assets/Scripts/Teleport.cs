using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform teleB;
    public int row1, column1;
    public int row2, column2;
    // Use this for initialization
	void Start () {
       // transform.position = new Vector2(row1, column1);
       // teleB.transform.position = new Vector2(row2, column2);    
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = teleB.transform.position;
        }
    }
   
}
