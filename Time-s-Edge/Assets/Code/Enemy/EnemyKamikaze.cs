using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : EnemyFather
{
    void Start()
    {
        SpeedEnemy = 0.08f;
        CurSpeedEnemy = SpeedEnemy;
        RotationSpeed = 2.0f;
        DamageKamikaze = 20;
        RegenHp = -10; //здоровье восполняемое за убийство с минусом
        SpeedEnemy = 0.08f;
        _curEnemyHp = 3;
        _rb = GetComponent<Rigidbody2D>();
        _playerCenter = GameObject.FindGameObjectWithTag("PlayerCenter").transform;
    }

    void Update()
    {

        CurSpeedEnemy += 0.01f * SpeedEnemy;
        if (CurSpeedEnemy > SpeedEnemy) { CurSpeedEnemy = SpeedEnemy; }

        //Временно убрал, пока не требуется, вычисление дистанции до игрока
        //var distanceToPlayer = Vector2.Distance(_player.position, transform.position);
        //if (distanceToPlayer < DistanceChase)
        //{
        _rb.MovePosition(Vector2.MoveTowards(_rb.position, _playerCenter.position, CurSpeedEnemy));
        Vector3 direction = _playerCenter.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageKamikaze, transform.position, -0.3f);
            Die();
        }
    }
}