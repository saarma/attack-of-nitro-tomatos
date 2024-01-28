using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
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
            other.GetComponent<Car>().SetOriginalPosition();
        }

        if (other.gameObject.CompareTag("Tomato"))
        {
            other.GetComponent<Enemy>().SetOriginalPosition();
        }
    }
}
