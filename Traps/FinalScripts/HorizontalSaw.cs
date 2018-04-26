using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSaw : MonoBehaviour {


    private float speed = 3;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * Time.deltaTime,0, 0);


    }



    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "GoLeft") //GoLeft is tag for RightCollider
        {
            speed = -3;

        }

        if (obj.gameObject.tag == "GoRight")   ////GoRight is tag for LeftCollider
        {

            speed = 3;
        }


    }




}
