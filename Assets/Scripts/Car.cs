using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    public float Velocity;

    [SerializeField]
    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Velocity = 50f;
        RotateSpeed = 100f;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
