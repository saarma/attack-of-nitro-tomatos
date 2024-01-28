using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countDown = 3.0f;
    public Wave[] waves;

    [SerializeField] private GameObject[] spawnPoints;

    private Wave CurrentWave;

    public TextMeshProUGUI waveCounter;

    private int lastWaveIndex = 0;
    public int currentWaveIndex = 0;
    private bool readyToCountDown;

    [SerializeField] GameObject scriptHolder;

   /// <summary>
   /// Onko peli voitettu?
   /// </summary>
    private bool GameIsAtTheEnd = false;

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
        

        if (waves[currentWaveIndex].enemiesLeft <= 0)
        {
            readyToCountDown = true;
            lastWaveIndex = currentWaveIndex;

            if(waves[currentWaveIndex].FinalWave) {
                GoToNextWave();
                GoToEnd();
            } else {
                GoToNextWave();
            }

            waveCounter.text = "Wave: " + (currentWaveIndex + 1);
        }      

    }

    private void GoToEnd() {
        //VICTORY!!
        //TODO: Activate pöydän tippuminen
        GameIsAtTheEnd = true;
    }

    private void GoToNextWave() {
        if(currentWaveIndex < waves.Length - 1) {
            currentWaveIndex++;

            scriptHolder.GetComponent<DoorMoveScript>().CloseTheDoors();
        }
    }

    private IEnumerator SpawnWave()
    {
        scriptHolder.GetComponent<DoorMoveScript>().OpenTheDoors();
        //Debug.Log("Spawn Wave");
        //Debug.Log(currentWaveIndex);
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].tomatoes.Length; i++)
            {
                var spawnPoint = GetRandomSpawnPoint();
                if(spawnPoint != null) {
                    Enemy enemy = Instantiate(waves[currentWaveIndex].tomatoes[i], spawnPoint.transform);
                    //enemy.transform.Rotate(180, 0, 180);
                    //Set variables for joints, change mass within range
                    enemy.transform.SetParent(spawnPoint.transform);
                    yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
                }
            }
        }
    }
    
    private GameObject GetRandomSpawnPoint() {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            // Get a random index within the array
            int randomIndex = Random.Range(0, spawnPoints.Length);

            // Get the random GameObject
            GameObject randomGameObject = spawnPoints[randomIndex];

            // Now you can use the random GameObject as needed
            Debug.Log("Random GameObject: " + randomGameObject.name);

            return randomGameObject;
        }
        else
        {
            Debug.LogWarning("The spawnPoints is either null or empty.");
            return null;
        }
    }

   

}