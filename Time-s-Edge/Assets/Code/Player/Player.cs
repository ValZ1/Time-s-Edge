using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioClip[] sound;
    public AudioSource audioData;
    public DamageBlinkEffect blinkEffect;

    public int StartHp = 60;
    public float SpeedPlayer = 0.1f;
    //TRAD - to read and delete - буду оставлять с этой аббревиатурой комментарии на некоторых незначительных изменениях. оставлять их не стоит
    //но стоит держать в уме
    private int CurHp;  //TRAD поменят тип с public на private, чтобы избежать багов связанных с бесконтрольным изменением здоровья.
                               //TRAD чтобы просмотреть здоровье игрока - get_CurHp(), изменить его - TakeDamage.
                               //TRAD думаю поменять тип на double, чтобы иметь возможность отнимать не 1хп/сек, а 1.5 и проч. приколы.
    private Vector2 _moveVector;

    private int _timeBurner = -1;
    private float _timer = 0f;
    private Rigidbody2D _rb;

    //dmg
    public float SpeedBullet = 10.0f;
    private int DamageBullet = 1;
    //защита
    [SerializeField] private double Protection = 0.0;

    [SerializeField] private double HealBoost = 0.0;
    //ну а че вы хотели
    //4 blink
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
        //TRAD ИМХО, стоит перенести в отдельную функцию и постоянно вызывать ее в Update
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


        PushPlayer(pushFrom, pushPower);
        if (!isInvul) { 
            if (CurHp - (int)(damage * (1 - Protection)) <= 0) //ситуация, в которой на 0.1 сек у персонажа отрицательное хп теперь не проблема
                Die();
            else 
            {
                
                audioData.clip = sound[Random.Range(0,4)];
                audioData.Play();

                CurHp -= (int)(damage * (1 - Protection));
                if (blinkEffect != null)
                {
                    blinkEffect.StartBlink();
                }

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

    public void PushPlayer(Vector2 pushFrom, float pushPower)
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