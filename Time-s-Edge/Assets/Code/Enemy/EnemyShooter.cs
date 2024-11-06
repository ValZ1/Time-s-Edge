using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float SpeedEnemy = 0.08f;
    public float RotationSpeed = 2.0f;
    public int DamageKamikaze = 50;
    public float DistanceShoot = 4.0f;

    private Transform _player;
    private Rigidbody2D _rb;
    private int _curEnemyHp;

    public GameObject PrefabBullet;
    public Transform ArmCenter;
    public float MaxCooldownTime = 1.0f;
    public float MaxCooldownChaseTime = 2.0f;

    private float _cooldownTime;
    private float _cooldownChaseTime;
    void Start()
    {
        _curEnemyHp = 100;
        _cooldownTime = MaxCooldownTime;
        _cooldownChaseTime = MaxCooldownChaseTime;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector2.Distance(_player.position, transform.position);
        if (distanceToPlayer > DistanceShoot && _cooldownChaseTime >= MaxCooldownChaseTime)
        {
            _rb.MovePosition(Vector2.MoveTowards(_rb.position, _player.position, SpeedEnemy));
        }
        else if (distanceToPlayer <= DistanceShoot && _cooldownTime >= MaxCooldownTime)
        {
            //В будущем требует доработки, попытаюсь реализовать стрельбу в сторону движения игрока
            Vector3 direction = _player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            ArmCenter.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
            _cooldownTime = 0.0f;
            _cooldownChaseTime = 0.0f;
        }
        _cooldownTime += Time.deltaTime;
        _cooldownChaseTime += Time.deltaTime;
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
