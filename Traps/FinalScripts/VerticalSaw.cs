using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSaw : MonoBehaviour {

    // 

    private float speed = 3;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);

    }



    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "GoUp")  //GoUp is tag for UpCollider
        {
            speed = 3;

        }

        if (obj.gameObject.tag == "GoDown")     //GoDown is tag for DownCollider
        {

            speed = -3;
        }


    }
}
