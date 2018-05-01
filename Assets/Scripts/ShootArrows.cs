using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour 
{
    public GameObject arrows;
    public Transform spawnPoint;
    public float time = 0;
    public float delay;

    void Update()
    {
        if (time < Time.time)
        {
            Instantiate(arrows, spawnPoint.position, spawnPoint.rotation, this.transform);
            time = Time.time + delay;
        }
    }
}
