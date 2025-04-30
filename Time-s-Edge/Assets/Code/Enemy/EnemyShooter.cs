using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyShooter : EnemyFather
{
    private float angle;
    protected override void Start()
    {
        base.Start();

        SpeedEnemy = 0.04f;
        CurSpeedEnemy = SpeedEnemy;
        RotationSpeed = 2.0f;
        DamageKamikaze = 20;
        DistanceShoot = 4.0f;
        RegenHp = -20;
        MaxCooldownTime = 1.0f;
        MaxCooldownChaseTime = 2.0f;
        _curEnemyHp = 4;
        _cooldownTime = MaxCooldownTime;
        _cooldownChaseTime = MaxCooldownChaseTime;
        _rb = GetComponent<Rigidbody2D>();
        _playerCenter = GameObject.FindGameObjectWithTag("PlayerCenter").transform;
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        CurSpeedEnemy += 0.01f * SpeedEnemy;
        if (CurSpeedEnemy > SpeedEnemy) { CurSpeedEnemy = SpeedEnemy; }
        // ќтключаем движени€, чтобы при столкновении со своими сородичами не летал по всей карте,
        // а также позвол€ет ему рассталкивать других стрелков, чтоб достичь игрока
        _rb.linearVelocity = Vector2.zero;
        var distanceToPlayer = Vector2.Distance(_playerCenter.position, transform.position);
        if (distanceToPlayer > DistanceShoot && _cooldownChaseTime >= MaxCooldownChaseTime)
        {
            animator.SetBool("isEnemyMoving", true);
            _rb.MovePosition(Vector2.MoveTowards(_rb.position, _playerCenter.position, CurSpeedEnemy));
        }
        else if (distanceToPlayer <= DistanceShoot && _cooldownTime >= MaxCooldownTime)
        {
            animator.SetBool("isEnemyMoving", false);
            //¬ будущем требует доработки, попытаюсь реализовать стрельбу в сторону движени€ игрока
            Vector3 direction = _playerCenter.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            ArmCenter.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed);
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
            _cooldownTime = 0.0f;
            _cooldownChaseTime = 0.0f;
        }
        CheckFlipX(angle);
        _cooldownTime += Time.deltaTime;
        _cooldownChaseTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageKamikaze, transform.position, 0.3f);
            Die();
        }
    }
}
