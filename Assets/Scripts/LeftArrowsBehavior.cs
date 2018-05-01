using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrowsBehavior : MonoBehaviour {

    public float speed;
    
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (obj.gameObject.tag == "Exit")
        {
            Destroy(gameObject);
        }
        if (obj.gameObject.tag == "Trap")
        {
            Destroy(gameObject);
        }
    }
}
