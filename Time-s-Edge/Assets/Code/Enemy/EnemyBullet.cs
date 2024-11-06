using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float SpeedBullet = 10.0f;
    public int DamageBullet = 20;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.right * (SpeedBullet * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(DamageBullet);
            Destroy(gameObject);
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
