using UnityEngine;

public class ReturningBullet : MonoBehaviour
{
    public float bulletSpeed = 6f; 
    public bool isReturning = false;
    private Transform bossTransform;
    private GameObject boss;
    void Start()
    {
       
        boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            bossTransform = boss.transform;
        }

        GetComponent<Rigidbody2D>().linearVelocity = transform.right * bulletSpeed;
    }

    void Update()
    {
        if (boss == null)
            Destroy(gameObject);

        if (isReturning)
        {
            Vector2 direction = (bossTransform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        }
        else 
        {
            Vector2 direction = (bossTransform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = -direction * bulletSpeed;
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(10, transform.position, 0.3f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss") && isReturning == true)
        {
            Destroy(gameObject);
        }
       
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isReturning = true;
        }

        else if (collision.gameObject.CompareTag("vase"))
        {
            isReturning = true;
        }
        else if (collision.gameObject.CompareTag("decoration"))
        {
            isReturning = true;
        }


    }
}