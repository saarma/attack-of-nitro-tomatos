using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private float countDown = 3.0f;
    public Wave[] waves;

    [SerializeField] private GameObject spawnPoint;

    public int currentWaveIndex = 0;
    private bool readyToCountDown;



    // Update is called once per frame
    void Update()
    {

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("Every tomato is dead");
            return;
        }

        if (readyToCountDown == true)
        {
            countDown -= Time.deltaTime;
        }

        if (countDown <= 0)
        {
            readyToCountDown = false;
            countDown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }


        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }

    }
    private IEnumerator SpawnWave()
    {
        Debug.Log("Spawn Wave");
        Debug.Log(currentWaveIndex);
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].tomatoes.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].tomatoes[i], spawnPoint.transform);
                //Set variables for joints, change mass within range
                enemy.transform.SetParent(spawnPoint.transform);
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
    void Start()
    {
        readyToCountDown = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].tomatoes.Length;
        }

    }

   

}