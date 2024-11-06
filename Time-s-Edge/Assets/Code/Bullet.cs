using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        //if (other.TryGetComponent(out Enemy enemy))
        //{
        //    enemy.TakeDamage(damageBullet);
        //    Destroy(gameObject);
        //}
        if (other.TryGetComponent(out EnemyKamikaze enemy))
        {
            enemy.TakeDamage(DamageBullet);
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out EnemyShooter enemyShooter))
        {
            enemyShooter.TakeDamage(DamageBullet);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
