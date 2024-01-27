using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestRoyTagObjectOnHit : MonoBehaviour
{
    // Public flag to choose the mode (destroy or deactivate)
    public bool destroyOnCollision = true;
    // Public flag to set mode
    public bool useCollisionEnter = true;
    public bool useCollisionTricker = true;

    public string specificTag = "Enemy";

    public void OnCollisionEnter(Collision collision)
    {
        if(!useCollisionEnter)
        {
            return;
        }

        if (collision.gameObject.CompareTag(specificTag))
        {
            if (destroyOnCollision)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
    public void OnTrickernEnter(Collision collision)
    {
        if (!useCollisionTricker)
        {
            return;
        }

        if (collision.gameObject.CompareTag(specificTag))
        {
            if (destroyOnCollision)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
