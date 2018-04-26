using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrowsBehavior : MonoBehaviour {

    //Script belongs to LeftArrow. Makes arrow shoot left.
   
    public float speed;


    void Update()
    {

        transform.Translate(-speed * Time.deltaTime, 0, 0);
       
    }
}
