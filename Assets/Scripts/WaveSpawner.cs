using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private float countDown = 3.0f;
    public Wave[] waves;

    private Wave CurrentWave;

    [SerializeField] private GameObject spawnPoint;

    public TextMeshProUGUI waveCounter;

    private int lastWaveIndex = 0;
    public int currentWaveIndex = 0;
    private bool readyToCountDown;

    void Start()
    {
        readyToCountDown = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].tomatoes.Length;
        }

        //set first wave
        CurrentWave = waves[0];
        waveCounter.text = "Wave: " + (currentWaveIndex + 1);
    }

    public Wave GetCurrentWave() {
        return CurrentWave;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveIndex != lastWaveIndex) {
            CurrentWave = waves[currentWaveIndex];
        }

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
            lastWaveIndex = currentWaveIndex;
            currentWaveIndex++;

            waveCounter.text = "Wave: " + (currentWaveIndex + 1);
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
                //enemy.transform.Rotate(180, 0, 180);
                //Set variables for joints, change mass within range
                enemy.transform.SetParent(spawnPoint.transform);
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
    

   

}