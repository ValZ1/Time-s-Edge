using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    public Player player;
    public float SpeedEnemy = 0.04f;
    public float RotationSpeed = 3.0f;
    public int DamageTouch = 20;
    public float TouchDelay = 1f;
    public float CurrentDelay = 0f;
    public int vampiric = -40; //здоровье восполняемое за убийство с минусом
    //public float DistanceChase = 10.0f;

    private Transform _player;
    private Rigidbody2D _rb;
    private int _curEnemyHp;
    // Start is called before the first frame update
    void Start()
    {
        _curEnemyHp = 12;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        //Временно убрал, пока не требуется, вычисление дистанции до игрока
        //var distanceToPlayer = Vector2.Distance(_player.position, transform.position);
        //if (distanceToPlayer < DistanceChase)
        //{
        _rb.MovePosition(Vector2.MoveTowards(_rb.position, _player.position, SpeedEnemy));
        Vector3 direction = _player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
        if (CurrentDelay >= 0f) 
            CurrentDelay -= Time.deltaTime;
        //}
    }
    //TODO функция отбрасывания игрока от объекта на n метров
    public void TakeDamage(int damage)
    {
        _curEnemyHp -= damage;
        if (_curEnemyHp <= 0)
        {
            player.TakeDamage(vampiric);
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && CurrentDelay <= 0f)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageTouch);
            TakeDamage(1);
            CurrentDelay = TouchDelay;
            //TODO вызов функции отбрасывания игрока
        }
    }
}