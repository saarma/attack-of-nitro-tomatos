using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed;

    private WaveSpawner waveSpawner;

    private int HP = 1;
    private int AttackPower = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waveSpawner = GetComponentInParent<WaveSpawner>();
 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

}