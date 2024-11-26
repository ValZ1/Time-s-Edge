using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int StartHp = 60;
    public float SpeedPlayer = 0.1f;
    //TRAD - to read and delete - буду оставлять с этой аббревиатурой комментарии на некоторых незначительных изменениях. оставлять их не стоит
    //но стоит держать в уме
    private static int CurHp;  //TRAD поменят тип с public на private, чтобы избежать багов связанных с бесконтрольным изменением здоровья.
                               //TRAD чтобы просмотреть здоровье игрока - get_CurHp(), изменить его - TakeDamage.
                               //TRAD думаю поменять тип на double, чтобы иметь возможность отнимать не 1хп/сек, а 1.5 и проч. приколы.
    public static Vector2 _moveVector;

    private int _timeBurner = -1;
    private float _timer = 0f;
    private Rigidbody2D _rb;

    //dmg
    public float SpeedBullet = 10.0f;
    public static int DamageBullet = 1;

    //ну а че вы хотели
    //4 blink
    public static float _blink_Range = 11.5f;
    public static int _you_Have_Blink = 0;
    public static float _cooldown_Blink = 5.0f;
    public static float _cur_Cooldown_Blink = 5.0f;
    public float _blink_duration = 0.3f;
    public int flag = 0;

    //Анимация
    public PlayerArm PlayerArm;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
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
        Destroy(gameObject); //Не забыть добавить скрин GAME OVER!!!
        SceneManager.LoadScene("SampleScene");
    }
    /// <summary>
    /// наносит игроку урон в размере damage. Леченим считать отрицательный урон.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        CheckDie(); 
    }
    //Getters
    public int get_CurHp(){return CurHp;}
    public int get_Damage(){return DamageBullet;}
    public float get_cooldown_blink() { return _cooldown_Blink; }
    //Setters
    public void damage_Up(int damage){ DamageBullet += damage;}
    public void blink_Up() { _you_Have_Blink = 1;}
    public void blink_range_Up(float range) {_blink_Range += range;}
    public void blink_cooldown_reduction(float reduction) { _cooldown_Blink -= _cooldown_Blink * reduction; }
}