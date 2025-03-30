using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : EnemyFather
{
    private float angle;
    protected override void Start()
    {
        base.Start();

        SpeedEnemy = 0.04f;
        CurSpeedEnemy = SpeedEnemy;
        RotationSpeed = 3.0f;
        DamageTouch = 20;
        TouchDelay = 1f;
        CurrentDelay = 0f;
        RegenHp = -40; //здоровье восполняемое за убийство с минусом
        SpeedEnemy = 0.08f;
        _curEnemyHp = 3;
        _rb = GetComponent<Rigidbody2D>();
        _playerCenter = GameObject.FindGameObjectWithTag("PlayerCenter").transform;
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
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
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
        if (CurrentDelay >= 0f) 
            CurrentDelay -= Time.deltaTime;
        CheckFlipX(angle);
        //}
        animator.SetBool("isEnemyMoving", true);
    }
    //TODO функция отбрасывания игрока от объекта на n метров
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CurrentDelay <= 0f)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageTouch, transform.position, -0.3f);
            CurrentDelay = TouchDelay;
            
            //TODO вызов функции отбрасывания игрока
        }
    }
}