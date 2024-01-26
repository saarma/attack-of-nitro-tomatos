using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tomato;
    float spawnTimer = 1f;
    float timer = 0f;
    int waveNumber = 0;



    public void SpawnRandom()
    {
        Instantiate(tomato, this.transform.position, Quaternion.identity);
    }
    void Start()
    {
        tomato = GameObject.FindGameObjectWithTag("Tomato");
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            SpawnRandom();
            timer = 0f;
        }
        
    }

}

