using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MockMenuCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            startGame();
        }
       
    }

    void startGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
