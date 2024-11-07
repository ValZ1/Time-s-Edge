using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    
    public float SpeedPlayer = 0.1f;
<<<<<<< Updated upstream

=======
    public static int hp = 60; //
    public static int _regeneration = -2; // регенерация (статик в будущем убрать)
    private float _timer = 0f;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }
=======

        //Механика времени-хп
        _timer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с последнего кадра

        if (_timer >= 1f)
        {
            _timer = 0f; // Сбрасываем таймер
            hp += _regeneration;
        }

        if(hp <= 0)
        { 
            Destroy(gameObject); //Не забыть добавить скрин GAME OVER!!!
        }


    }

    
>>>>>>> Stashed changes

    //Функцию надо доработать, когда будет добавлена переменная времени (жизни)
    public void TakeDamage(int damage)
    {
<<<<<<< Updated upstream
        //Destroy(gameObject);
=======
        hp -= damage;
>>>>>>> Stashed changes
    }
    //TODO
    //сделать функцию для регенерации
}