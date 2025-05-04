using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player player;
    

    void Start()
    {
        player = FindFirstObjectByType<Player>();
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
        if (other.TryGetComponent(out EnemyFather enemy))
        {
            enemy.TakeDamage(player.get_Damage());
            enemy.PushAway(transform.position, -0.03f);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("decoration"))
        {
            Destroy(gameObject);
        }

    }
}
