using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player player;
    

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.right * (player.SpeedBullet * Time.deltaTime);
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
            enemy.TakeDamage(player.get_Damage());
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out EnemyShooter enemyShooter))
        {
            enemyShooter.TakeDamage(player.get_Damage());
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out EnemyTank enemyTank))
        {
            enemyTank.TakeDamage(player.get_Damage());
            Destroy(gameObject);
        }
        else if (other.TryGetComponent(out target_dummy td))
        {
            td.TakeDamage(player.get_Damage());
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
