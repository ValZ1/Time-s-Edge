using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet = 1.0f;
    public int damageBullet = 20;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position += transform.right * (speedBullet * Time.deltaTime);
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
            enemy.TakeDamage(damageBullet);
            Destroy(gameObject);
        }
    }
}
