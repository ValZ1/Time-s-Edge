using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFather : MonoBehaviour
{
    public Player player;
    public float SpeedEnemy;
    public float CurSpeedEnemy;
    public float RotationSpeed;
    public float PushResistance = 0.0f;

    //4 kmkz
    public int DamageKamikaze;
    public int RegenHp; //�������� ������������ �� �������� � �������

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
    public int _curEnemyHp;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    // Update is called once per frame


    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void PushTest()
    {
        var l = new Vector2(3000, 3000);
        _rb.AddForce(l * 3999);
    }
    protected void CheckFlipX(float angle)
    {
        bool shouldFlip = angle < 90f && angle > -90f;
        if (shouldFlip)
        {
            gameObject.transform.rotation = new Quaternion(0,180,0,0);
        }
        else gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

    }
    public void PushAway(Vector2 pushFrom, float pushPower)
    {
        // ���� ��� ������������� Rigidbody2D, �� ������ �� �������
        if (_rb == null || pushPower == 0)
        {
            return;
        }
        // ���������� � ����� ����������� ������ �������� ������
        // � ����� ����������� ���� ������, ����� ����� ���� ����� ������� ���� "�������"
        var pushDirection = (pushFrom - new Vector2(transform.position.x, transform.position.y)).normalized;


        _rb.AddForce(pushDirection * pushPower);
        CurSpeedEnemy = 0f;
    }

    public void TakeDamage(int damage)
    {
        _curEnemyHp -= damage;



        if (_curEnemyHp <= 0)
        {
            player.Heal(-RegenHp);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
