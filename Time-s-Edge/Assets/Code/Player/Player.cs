using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float SpeedPlayer = 0.1f;

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
    }

    //Функцию надо доработать, когда будет добавлена переменная времени (жизни)
    public void TakeDamage(int damage)
    {
        //Destroy(gameObject);
    }
}