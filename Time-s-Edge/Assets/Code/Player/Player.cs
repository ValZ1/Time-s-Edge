using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int StartHp = 60;
    public float SpeedPlayer = 0.1f;
    //TRAD - to read and delete - буду оставлять с этой аббревиатурой комментарии на некоторых незначительных изменениях. оставлять их не стоит
    //но стоит держать в уме
    private int CurHp;  //TRAD поменят тип с public на private, чтобы избежать багов связанных с бесконтрольным изменением здоровья.
                               //TRAD чтобы просмотреть здоровье игрока - get_CurHp(), изменить его - TakeDamage.
                               //TRAD думаю поменять тип на double, чтобы иметь возможность отнимать не 1хп/сек, а 1.5 и проч. приколы.
    public static Vector2 _moveVector;

    private int _timeBurner = -1;
    private float _timer = 0f;
    private Rigidbody2D _rb;

  
    void Start()
    {
        CurHp = StartHp;
        _rb = GetComponent<Rigidbody2D>();
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

        Burner_by_time();


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
            ChackDie();
        }
    }

    //TRAD зачем? чтобы не проверять это в Update каждый кадр. Герой получает урон 1 раз в секунду гарантированно и очень редко от других источников.
    //TRAD Неразумно проврять этот показатель каждый кадр
    /// <summary>
    /// Проверяет хп героя. если оно <=0 то убивает его
    /// </summary>
    void ChackDie()
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
    /// Возвращает текущее здоровье игрока
    /// </summary>
    /// <returns></returns>
    public int get_CurHp()
    {
        return CurHp;
    }
    /// <summary>
    /// наносит игроку урон в размере damage. Леченим считать отрицательный урон.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        CurHp -= damage;
        ChackDie(); 
    }
}