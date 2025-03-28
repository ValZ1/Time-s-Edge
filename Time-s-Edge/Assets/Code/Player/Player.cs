using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioClip[] sound;
    public AudioSource audioData;


    public int StartHp = 60;
    public float SpeedPlayer = 0.1f;
    private int CurHp;  
    private Vector2 _moveVector;

    public int _timeBurner = -1; //паблик потому что лень методы делать)
    private float _timer = 0f;
    private Rigidbody2D _rb;

    [Header("Item Detection")]
    [SerializeField] private float itemDetectionRange = 2f; // Радиус обнаружения предметов
    private List<ItemFather> nearestItem = new List<ItemFather>(); // Ближайший предмет
    private ItemFather closestItem;
    //dmg
    public float SpeedBullet = 10.0f;
    private int DamageBullet = 1;
    //защита
    [SerializeField] private double Protection = 0.0;

    [SerializeField] private double HealBoost = 0.0;
    [SerializeField] private float _blink_Range = 11.5f;
    [SerializeField] private int _you_Have_Blink = 0;
    [SerializeField] private float _cooldown_Blink = 5.0f;
    [SerializeField] private float _cur_Cooldown_Blink = 5.0f;
    public float _blink_duration = 0.3f;
    public int flag = 0;

    public bool isInvul = false;
    public float invultime = 0;
    //Анимация
    public PlayerArm PlayerArm;
    public SpriteRenderer ArmSpriteRenderer;

    private Animator animator;
    private SpriteRenderer spriteRenderer;



    //
    Vector2 pushDirection;
    bool is_push = false;
    float push_time = 2.99f;
    float delta = 1.01f;
    int timer = 0;
    void Start()
    {
        audioData = GetComponent<AudioSource>();

        CurHp = StartHp;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
        //Передвижение игрока
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        _moveVector = new Vector2(horiz, vert);
        if (_moveVector.magnitude > 1)
        {
            _moveVector.Normalize();
        }
        _rb.MovePosition(_rb.position + _moveVector * SpeedPlayer);


        if (_you_Have_Blink == 1) 
        { 
            if (Input.GetKeyDown(KeyCode.Z) && _cur_Cooldown_Blink >= _cooldown_Blink)
            {
                _cur_Cooldown_Blink = 0;
                flag = 1;
            }
            if (flag == 1)
            {
                _blink_duration -= Time.deltaTime;
                _rb.MovePosition(_rb.position + _moveVector * _blink_Range / 26); //26 нужно на что то поменять
            }
            if (_blink_duration <= 0)
            {
                flag = 0;
                _blink_duration = 0.3f;
            }
            _cur_Cooldown_Blink += Time.deltaTime;
        }
        Burner_by_time();

        if (isInvul && invultime < 1f)
        {
            invultime += Time.deltaTime;
        }
        if (invultime >= 1f) { 
            isInvul = false;
            invultime = 0f;
        }

        if (is_push)
        {
            if (timer == 3)
            {
                _rb.AddForce(pushDirection * Time.deltaTime / delta);
                timer = 0;
            }
            else
                {
                timer += 1;
            }
            delta *= 1.01f;
            push_time -= Time.deltaTime;
            if (push_time <= 0)
            {
                is_push = false;
                push_time = 0.99f;
                delta = 1.01f;
            }


            DetectNearbyItems();
            UpdateClosestItem();

        }

        //Анимация движения, будет проигрываться, когда игрок двигается
        animator.SetBool("isMoving", _moveVector.magnitude > 0);
        CheckFlipX();
    }
    /// <summary>
    /// Функция поворота игрока в сторону мыши
    /// </summary>
    void CheckFlipX()
    {
        bool shouldFlip = PlayerArm.get_targetAngle() > 90f || PlayerArm.get_targetAngle() < -90f;
        if (spriteRenderer.flipX != shouldFlip)
        {
            spriteRenderer.flipX = shouldFlip;
            ArmSpriteRenderer.flipY = shouldFlip;
        }
    }



    private void DetectNearbyItems()
    {
        nearestItem.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, itemDetectionRange);

        foreach (var collider in colliders)
        {
            ItemFather item = collider.GetComponent<ItemFather>();
            if (item != null)
            {
                nearestItem.Add(item);
            }
        }
    }

    /// <summary>
    /// Находит ближайший предмет и обновляет UI
    /// </summary>
    private void UpdateClosestItem()
    {
        ItemFather newClosest = null;
        float minDistance = Mathf.Infinity;

        foreach (var item in nearestItem)
        {
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                newClosest = item;
            }
        }

        // Если ближайший предмет изменился
        if (closestItem != newClosest)
        {
            closestItem = newClosest;
            UpdateItemDisplay();
        }
    }

    /// <summary>
    /// Обновляет отображение информации о предмете
    /// </summary>
    private void UpdateItemDisplay()
    {
        if (ItemInfoDisplay.Instance != null)
        {
            if (closestItem != null)
            {
                ItemInfoDisplay.Instance.ShowItemInfo(closestItem);
            }
            else
            {
                ItemInfoDisplay.Instance.HideInfo();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса обнаружения
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, itemDetectionRange);

        // Линия к ближайшему предмету
        if (closestItem != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, closestItem.transform.position);
        }
    }


    /// <summary>
    /// Хваленая механика постоянной потери здоровья
    /// </summary>
    void Burner_by_time()
    {
        //Механика времени-хп
        _timer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с последнего кадра

        if (_timer >= 1f)
        {
            _timer = 0f; // Сбрасываем таймер
            CurHp += _timeBurner;//TRAD думаю поменять на TakeDamage(-_timeBurner)
            //TRAD решил перенести сюда, чтобы не проверять каждый deltaTime, добавлю во все места в коде, где этого еще нет
            CheckDie();
        }
    }

    //TRAD зачем? чтобы не проверять это в Update каждый кадр. Герой получает урон 1 раз в секунду гарантированно и очень редко от других источников.
    //TRAD Неразумно проврять этот показатель каждый кадр
    /// <summary>
    /// Проверяет хп героя. если оно <=0 то убивает его
    /// </summary>
    void CheckDie()
    {
        if (CurHp <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// Мгновенное убийство игрока
    /// </summary>
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //Destroy(gameObject); //Не забыть добавить скрин GAME OVER!!!

    }
    /// <summary>
    /// наносит игроку урон в размере damage. Леченим считать отрицательный урон.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage, Vector2 pushFrom, float pushPower)
    {


        if (!isInvul) { 
            if (CurHp - (int)(damage * (1 - Protection)) <= 0) //ситуация, в которой на 0.1 сек у персонажа отрицательное хп теперь не проблема
                Die();
            else 
            {
                pushDirection = -(pushFrom - new Vector2(transform.position.x, transform.position.y)).normalized;
                is_push = true;

                audioData.clip = sound[Random.Range(0,4)];
                audioData.Play();

                CurHp -= (int)(damage * (1 - Protection));
                isInvul = true;
            }
        }
    }

    private void invulnerability(float invulTime)
    {
        //TODO анимация


    }    

    public void Heal(int damage)
    {
        CurHp += damage; //(int)(damage * (1 + HealBoost));
    }



    /// <summary>
    /// используется при покупке предметов
    /// </summary>
    /// <param name="price"></param>
    public void Buy(int price){CurHp -= price;} 
    //Getters
    public int get_CurHp() => CurHp;
    public int get_Damage() => DamageBullet;
    public float get_cooldown_blink() => _cooldown_Blink;
    //Setters
    public void damage_Up(int damage){ DamageBullet += damage;}
    public void protection_Up(double protection) { Protection += protection ; } // * (1 - Protection)
    public void Heal_Up(double healBoost) { HealBoost += healBoost; }

    public void blink_Up() { _you_Have_Blink = 1;}
    public void blink_range_Up(float range) {_blink_Range += range;}
    public void blink_cooldown_reduction(float reduction) { _cooldown_Blink -= _cooldown_Blink * reduction; }
}