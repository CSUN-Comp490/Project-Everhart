using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour {


    public GameObject LeftArrows;
    public GameObject RightArrows;
   
    public Transform RightSpawnPoint;
    public Transform LeftSpawnPoint;
   


    public float time = 0;
    public float delay;

  
  
    

    void Update()
    {
        if (time < Time.time)
        {

            Instantiate(RightArrows, RightSpawnPoint.position, RightSpawnPoint.rotation);
            Instantiate(LeftArrows,LeftSpawnPoint.position,LeftSpawnPoint.rotation);
            
            time = Time.time + delay;
        }



    }
}
