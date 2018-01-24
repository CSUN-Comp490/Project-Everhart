using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            startOver();
        }
        Invoke("startOver", 3);

    }

    void startOver()
    {
        SceneManager.LoadScene("Menu1");
    }
}
