using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float SpeedPlayer = 0.1f;
    public int hp = 300;

    private float _timer = 0f;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //������������ ������
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        _moveVector = new Vector2(horiz, vert);
        if (_moveVector.magnitude > 1)
        {
            _moveVector.Normalize();
        }
        _rb.MovePosition(_rb.position + _moveVector * SpeedPlayer);

        //�������� �������-��
        _timer += Time.deltaTime; // ����������� ������ �� �����, ��������� � ���������� �����

        if (_timer >= 1f)
        {
            _timer = 0f; // ���������� ������
            hp--;
        }

        if(hp <= 0)
        { 
            Destroy(gameObject); //�� ������ �������� ����� GAME OVER!!!
        }


    }

    public void PlayerHp(int damage = 0)
    {
        hp -= damage;
    }

    //������� ���� ����������, ����� ����� ��������� ���������� ������� (�����)
    public void TakeDamage(int damage)
    {
        PlayerHp(damage);
    }
}