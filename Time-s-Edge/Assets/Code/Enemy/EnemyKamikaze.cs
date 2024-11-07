using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyKamikaze : MonoBehaviour
{
    public Player player;
    public float SpeedEnemy = 0.08f;
    public float RotationSpeed = 2.0f;
<<<<<<< Updated upstream
    public int DamageKamikaze = 50;
=======
    public int DamageKamikaze = 20;
    public int Vampiric = -10;
>>>>>>> Stashed changes
    //public float DistanceChase = 10.0f;

    private Transform _player;
    private Rigidbody2D _rb;
    private int _curEnemyHp;
    // Start is called before the first frame update
    void Start()
    {
        _curEnemyHp = 3;
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
        //}
    }
    public void TakeDamage(int damage)
    {
        _curEnemyHp -= damage;
        if (_curEnemyHp <= 0)
        {
            Die();
            player.TakeDamage(Vampiric);
        }    
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
