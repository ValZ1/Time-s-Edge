using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int StartHp = 60;
    public float SpeedPlayer = 0.1f;
    public static int CurHp;
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
        //Передвижение игрока
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        _moveVector = new Vector2(horiz, vert);
        if (_moveVector.magnitude > 1)
        {
            _moveVector.Normalize();
        }
        _rb.MovePosition(_rb.position + _moveVector * SpeedPlayer);

        //Механика времени-хп
        _timer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с последнего кадра

        if (_timer >= 1f)
        {
            _timer = 0f; // Сбрасываем таймер
            CurHp += _timeBurner;
        }

        if (CurHp <= 0)
        {
            Destroy(gameObject); //Не забыть добавить скрин GAME OVER!!!
            SceneManager.LoadScene("SampleScene");
        }


    }

    public void TakeDamage(int damage)
    {
        CurHp -= damage;
    }
}