using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField]
    public GameObject Car;

    [SerializeField]
    public GameObject Tomato;

    [SerializeField]
    public AudioSource HorrorAudio;

    [SerializeField]
    public Canvas Menu;

    [SerializeField]
    public Light SpotlightLeft;

    [SerializeField]
    public Light SpotlightRight;

    [SerializeField]
    public ParticleSystem FireLeft;

    [SerializeField]
    public ParticleSystem FireRight;

    public float ParticleTimer = 2f;
    public bool ParticleTimerStarted = false;

    [SerializeField]
    public GameObject Discoball;

    private Vector3 _discoballEndPosition = new Vector3(0, 1, 0);
    private float _discoballSpeed = 0f;
    public float DiscoballRotateSpeed = 20f;

    public float MenuStartTimer = 5.0f;
    private bool _timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //CarScript.Velocity = 50f;
        Menu.enabled = false;
        SpotlightLeft.intensity = 0;
        SpotlightRight.intensity = 0;
        FireLeft.Pause();
        FireRight.Pause();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("miikan", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = GetCarDistance();

        AdjustVolume(distance);

        if (_timerStarted)
        {
            SpotlightLeft.intensity = 5;
            SpotlightRight.intensity = 5;

            _discoballSpeed = 0.5f;
            MenuStartTimer -= Time.deltaTime;

            if (MenuStartTimer <= 0.0f)
            {
                Menu.enabled = true;
            }
        }

        if (ParticleTimerStarted)
        {
            ParticleTimer -= Time.deltaTime;

            if (ParticleTimer <= 0.0f)
            {
                FireLeft.Stop();
                FireRight.Stop();
            }
        }

        AdjustDiscoball();
    }

    private float GetCarDistance()
    {
        float distance = Vector3.Distance(Car.transform.position, Tomato.transform.localPosition);
        Debug.Log(distance);
        return distance;
    }

    private void AdjustVolume(float distance)
    {
        if (distance > 50f)
        {
            return;
        }

        if (distance < 3f)
        {
            HorrorAudio.Stop();
            Car.GetComponent<AudioSource>().spatialBlend = 0f;
            Car.GetComponent<AudioSource>().volume = 0.6f;

            //FireLeft.Play();
            //FireRight.Play();

            _timerStarted = true;
            return;
        }

        float multiplier = distance / 100.0f;

        //Car.GetComponent<AudioSource>().spatialBlend = Car.GetComponent<AudioSource>().spatialBlend * multiplier;
        HorrorAudio.volume = HorrorAudio.volume * multiplier / 2;

        Debug.Log(Car.GetComponent<AudioSource>().spatialBlend);
    }

    private void AdjustDiscoball()
    {
        var speed = _discoballSpeed * Time.deltaTime;
        Discoball.transform.position = Vector3.MoveTowards(Discoball.transform.position, _discoballEndPosition, speed);

        if (Discoball.transform.position == _discoballEndPosition)
        {
            ParticleTimerStarted = true;

            FireLeft.Play();
            FireRight.Play();
        }

        Discoball.transform.Rotate(Vector3.up * DiscoballRotateSpeed * Time.deltaTime);
    }
}
