using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countDown = 3.0f;
    public Wave[] waves;

    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField]
    public GameObject Booth;

    private Wave CurrentWave;

    public TextMeshProUGUI waveCounter;

    public GameObject endCreditsPanel;

    public GameObject car;

    private int lastWaveIndex = 0;
    public int currentWaveIndex = 0;
    private bool readyToCountDown;

    [SerializeField] GameObject scriptHolder;

   /// <summary>
   /// Onko peli voitettu?
   /// </summary>
    private bool _creditTimerEnabled = false;

    private float _creditsTimer = 6.66f; //seconds
    public CreditTextsController creditTextsController;

    public AudioClip Intro;
    public AudioClip Music;

    private bool _isIntroPlaying = false;

    void Start()
    {
        var rb = Booth.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        readyToCountDown = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].tomatoes.Length;
        }

        //set first wave
        CurrentWave = waves[0];
        ShowWaveCount();
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
            //Debug.Log("Every tomato is dead");
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

            ShowWaveCount();
        } 

        if(_creditTimerEnabled) {

            if(_creditsTimer >= 0) {
                _creditsTimer -= Time.deltaTime;
            }

            if(_creditsTimer <= 0f) {
                if (! _isIntroPlaying)
                {
                    AnnounceDJ();
                } 
                
                if (_isIntroPlaying && !Booth.GetComponent<AudioSource>().isPlaying)
                {
                    Booth.GetComponent<AudioSource>().PlayOneShot(Music);
                    StartCredits();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GoToEnd();
            ShowWaveCount();
        }

    }

    private void ShowWaveCount() {
        //TODO: infinite wave
        waveCounter.text = "Wave: " + (currentWaveIndex + 1);
    }

    private void StartCredits() {
        ReleaseTheBooth();
        KillAllTheTomatoes();
        _musicIsPlayed = true;
        car.SetActive(false);
    }

    private void AnnounceDJ() {
        _isIntroPlaying = true;
        endCreditsPanel.SetActive(true);
        
        Booth.GetComponent<AudioSource>().PlayOneShot(Intro);
    }


    private void GoToEnd() {
        currentWaveIndex = waves.Length - 1;
        _creditTimerEnabled = true;
    }

    private void GoToNextWave() {
        if(currentWaveIndex < waves.Length - 1) {
            currentWaveIndex++;
            scriptHolder.GetComponent<DoorMoveScript>().CloseTheDoors();
        }
    }
    
    private void ShowCreditTexts()
    {
        creditTextsController.StartCreditTexts();
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
                if(_creditsTimer <= 0f) {
                    continue;
                }
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
            //Debug.Log("Random GameObject: " + randomGameObject.name);

            return randomGameObject;
        }
        else
        {
            Debug.LogWarning("The spawnPoints is either null or empty.");
            return null;
        }
    }

    private bool _musicIsPlayed = false;

    private void ReleaseTheBooth() {
        //activate music
        
        if(!_musicIsPlayed) {

            _musicIsPlayed = true;
            var rb = Booth.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void KillAllTheTomatoes() {
        
         GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Tomato");

         foreach(GameObject obj in taggedObjects) {

            obj.GetComponent<AudioSource>().mute = true;
            obj.GetComponent<Enemy>().KillTomato();
         }
    }
   

}