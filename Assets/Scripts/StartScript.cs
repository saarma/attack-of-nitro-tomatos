using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    [SerializeField]
    public GameObject Car;

    [SerializeField]
    public GameObject Tomato;

    [SerializeField]
    public AudioSource HorrorAudio;

    // Start is called before the first frame update
    void Start()
    {
        //CarScript.Velocity = 50f;
    }

    // Update is called once per frame
    void Update()
    {


        if (GetCarDistance() < 20f)
        {
            HorrorAudio.Stop();
        }
    }

    private float GetCarDistance()
    {
        float distance = Vector3.Distance(Car.transform.position, Tomato.transform.localPosition);
        Debug.Log(distance);
        return distance;
    }
}
