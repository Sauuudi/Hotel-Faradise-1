using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarambanoSpawner : MonoBehaviour
{
    public float spawnTime = 3f;
    public GameObject carambanoPrefab;

    void Start () {
        InvokeRepeating ("SpawnCarambano", spawnTime, spawnTime);
    }
       
    void SpawnCarambano()
    {
        var newBall = GameObject.Instantiate(carambanoPrefab, transform.position, transform.rotation);
    }
}
