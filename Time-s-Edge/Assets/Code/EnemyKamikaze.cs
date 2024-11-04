using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    public Transform Player;
    public float SpeedEnemy = 0.08f;
    public float RotationSpeed = 2.0f;
    public int DamageKamikaze = 50;
    public float DistanceChase = 10.0f;

    private Rigidbody2D _rb;
    private int _curEnemyHp;
    // Start is called before the first frame update
    void Start()
    {
        _curEnemyHp = 100;
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector2.Distance(Player.position, transform.position);
        if (distanceToPlayer < DistanceChase)
        {
            _rb.MovePosition(Vector2.MoveTowards(_rb.position, Player.position, SpeedEnemy));
            Vector3 direction = Player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
        }
    }
    public void TakeDamage(int damage)
    {
        _curEnemyHp -= damage;
        if (_curEnemyHp <= 0)
            Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageKamikaze);
            Die();
        }
    }
}
