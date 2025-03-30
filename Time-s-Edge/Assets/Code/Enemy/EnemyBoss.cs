using UnityEngine;

public class EnemyBoss : EnemyFather
{

    protected override void Start()
    {
        base.Start();

        SpeedEnemy = 0.00f;
        RotationSpeed = 2.0f;
        DistanceShoot = 16.0f;
        RegenHp = -200;
        MaxCooldownTime = 2.9f;
        _curEnemyHp = 45;
        _cooldownTime = MaxCooldownTime;
        _rb = GetComponent<Rigidbody2D>();
        _playerCenter = GameObject.FindGameObjectWithTag("PlayerCenter").transform;
    }
    float accuracy = 0.7f; 
    float spreadAngle = 22.0f; 

    int atack_num = 0;
    // Update is called once per frame
    void Update()
    {

        var distanceToPlayer = Vector2.Distance(_playerCenter.position, transform.position);


        if (_cooldownTime >= MaxCooldownTime)
        {
            atack_num = Random.Range(1, 3);

        }

        if (distanceToPlayer <= DistanceShoot && atack_num == 1 && _cooldownTime >= MaxCooldownTime)
        { 
            Vector3 direction = _playerCenter.position - transform.position;
            float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion baseRotation = Quaternion.Euler(0, 0, baseAngle);
            

            for (int i = 0; i < 7; i++)
            {
                float randomSpread = Random.Range(-spreadAngle, spreadAngle) * accuracy;
                Quaternion spreadRotation = Quaternion.Euler(0, 0, baseAngle + randomSpread);

                Instantiate(PrefabBullet, transform.position, spreadRotation);
            }
            _cooldownTime = 0;
        }
        
        
        if (atack_num == 2 && _cooldownTime >= MaxCooldownTime)
        {
            ShootRandomDirections(5);
            _cooldownTime = 0;
        }

        

        _cooldownTime += Time.deltaTime;
        _cooldownChaseTime += Time.deltaTime;
    }
    //22
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(DamageKamikaze, transform.position, -0.3f);
        }
    }


    public GameObject PrefabReturnBullet;
    void ShootRandomDirections(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);

            Vector2 direction = rotation * Vector2.right;

            float offsetDistance = 2.4f; 
            Vector2 spawnPosition = (Vector2)transform.position + direction * offsetDistance;

            GameObject bullet = Instantiate(PrefabReturnBullet, spawnPosition, rotation);

            ReturningBullet returningBullet = bullet.GetComponent<ReturningBullet>();
            returningBullet.isReturning = false; // Явно устанавливаем значение

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * returningBullet.bulletSpeed;
        }
    }
}
