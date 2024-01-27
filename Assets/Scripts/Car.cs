using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car : MonoBehaviour
{
    [SerializeField]
    public float Velocity = 4f;

    [SerializeField]
    public float VelocityStep = 1f;

    [SerializeField]
    public float RotateSpeed = 80f;

    //charging
    [SerializeField]
    private float chargeModifier = 1.5f;
    private bool charging = false;

    private float originalVelocity;
    private Vector3 originalPosition;

    //enemyhitcounter
    public TextMeshProUGUI countText; 
    private int enemyHitCounter = 0;

    [SerializeField]
    private WaveSpawner waveSpawner;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
        originalVelocity = Velocity;

        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        GoForward();

        SetCountText();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);

        if(other.gameObject.CompareTag("Tomato")) {
            Debug.Log("Hit tomato!");

            var enemy = other.gameObject;
            var enemyDiePosition = enemy.transform.position;
            enemy.SetActive(false);

            IncreaseEnemyHitCounter();

            //Log enemy die position
            Debug.Log("enemy die position x: " + enemyDiePosition.x);
            Debug.Log("enemy die position y: " + enemyDiePosition.y);
            Debug.Log("enemy die position z: " + enemyDiePosition.z);

            if(waveSpawner != null) {
                waveSpawner.GetCurrentWave().enemiesLeft--;
            }
        }
    }

    void GoForward() {
        this.transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * RotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Velocity += VelocityStep;
            //this.transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
            //this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Velocity));
        }

        if (Input.GetKey(KeyCode.S))
        {
            Velocity -= VelocityStep;
            //this.transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
            //this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Velocity));
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            this.transform.position = originalPosition;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            charging = !charging;
            ChangeChargeSpeed();
        }

        //For debugging
        if(Input.GetKeyDown(KeyCode.C)) {
            IncreaseEnemyHitCounter();
        }
    }

    void IncreaseEnemyHitCounter() {
        enemyHitCounter++;
    }

    void ChangeChargeSpeed() {
        if(charging)
        {
            Velocity *= chargeModifier;
        } else
        {
            Velocity = originalVelocity;
        }
    }

    void SetCountText() 
    { 
        if (countText != null)
        {
            countText.text = "Tomatoes splattered: " + enemyHitCounter.ToString();
        }
    }
}
