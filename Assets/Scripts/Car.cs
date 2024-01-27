using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Car : MonoBehaviour
{
    [SerializeField]
    public float Velocity = 50f;

    [SerializeField]
    public float RotateSpeed = 100f;

    private bool charging = false;

    private float originalVelocity;
    private Vector3 originalPosition;

    //enemyhitcounter
    public TextMeshProUGUI countText; 
    private int enemyHitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Velocity = 4f;
        RotateSpeed = 80f;

        originalPosition = this.transform.position;
        originalVelocity = Velocity;

        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if(charging)
        {
            Velocity *= 1.5f;
            charging = false;
        } else
        {
            Velocity = originalVelocity;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * RotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
            //this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Velocity));
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.R)) {
            this.transform.position = originalPosition;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            charging = true;
        }

        if(Input.GetKey(KeyCode.C)) {
            enemyHitCounter++;
        }
    }

    void SetCountText() 
    {
        countText.text =  "Tomatoes splattered: " + enemyHitCounter.ToString();
    }
}
