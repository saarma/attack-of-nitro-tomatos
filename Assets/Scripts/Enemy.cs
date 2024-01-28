using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float speed;
    public float BodyTimer = 3f;
    public bool IsDead = false;

    private int HP = 1;
    private int AttackPower = 1;
    private Vector3 _originalPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (IsDead)
        {
            BodyTimer -= Time.deltaTime;

            if (BodyTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void SetOriginalPosition()
    {
        this.transform.position = _originalPosition;
    }

    public void KillTomato()
    {
        IsDead = true;
        this.GetComponent<Collider>().enabled = false;

        var oldScale = this.GetComponent<Transform>().localScale;
        this.GetComponent<Transform>().localScale = new Vector3(oldScale.x, 5, oldScale.z);
    }
}