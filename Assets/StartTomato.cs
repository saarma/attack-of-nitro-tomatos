using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTomato : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            var oldScale = this.GetComponent<Transform>().localScale;
            this.GetComponent<Transform>().localScale = new Vector3(oldScale.x, 5, oldScale.z);
            this.GetComponent<Rigidbody>().mass = 1000;
        }
    }
}
