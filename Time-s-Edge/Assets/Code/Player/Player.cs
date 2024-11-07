using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float SpeedPlayer = 0.1f;
    public static int hp = 60;
    public static int _regeneration = -2; //убрать статик?
    private float _timer = 0f;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    void Start()
    {
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
            hp += _regeneration;
        }

        if (hp <= 0)
        {
            Destroy(gameObject); //Не забыть добавить скрин GAME OVER!!!
            SceneManager.LoadScene("SampleScene");
        }


    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}