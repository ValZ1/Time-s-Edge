using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    public Player player;
    public float SpeedEnemy ;
    public float CurSpeedEnemy;
    public float RotationSpeed ;
    public float PushResistance = 0.0f;

    //4 kmkz
    public int DamageKamikaze ;
    public int RegenHp ; //здоровье восполняемое за убийство с минусом

    //4 tank
    public int DamageTouch;
    public float TouchDelay;
    public float CurrentDelay;
    //public float DistanceChase = 10.0f;



    //4 shooter
    public GameObject PrefabBullet;
    public Transform ArmCenter;
    protected float MaxCooldownTime;
    protected float MaxCooldownChaseTime;
    protected float _cooldownTime;
    protected float _cooldownChaseTime;
    protected float DistanceShoot;




    protected Transform _playerCenter;
    protected Rigidbody2D _rb;
    protected int _curEnemyHp;

    // Start is called before the first frame update

    // Update is called once per frame


    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
    public void PushTest()
    {
        var l = new Vector2(3000, 3000);
        _rb.AddForce(l * 3999);
    }

    public void PushAway(Vector2 pushFrom, float pushPower)
    {
        // Если нет прикреплённого Rigidbody2D, то выйдем из функции
        if (_rb == null || pushPower == 0)
        {
            return;
        }
        // Определяем в каком направлении должен отлететь объект
        // А также нормализуем этот вектор, чтобы можно было точно указать силу "отскока"
        var pushDirection = (pushFrom - new Vector2(transform.position.x, transform.position.y)).normalized;

        
        _rb.AddForce(pushDirection * pushPower);
        CurSpeedEnemy = 0f;
    }

    public void TakeDamage(int damage)
    {
        _curEnemyHp -= damage;

        

        if (_curEnemyHp <= 0)
        {
            Debug.Log("Dsdsdsds");
            player.Heal(-RegenHp);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}