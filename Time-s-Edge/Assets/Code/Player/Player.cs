using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    
    public float SpeedPlayer = 0.1f;
    public static int hp = 60; //
    public static int _regeneration = -2; // ðåãåíåðàöèÿ (ñòàòèê â áóäóùåì óáðàòü)
    
    private float _timer = 0f;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Ïåðåäâèæåíèå èãðîêà
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        _moveVector = new Vector2(horiz, vert);
        if (_moveVector.magnitude > 1)
        {
            _moveVector.Normalize();
        }
        _rb.MovePosition(_rb.position + _moveVector * SpeedPlayer);
        //Ìåõàíèêà âðåìåíè-õï
        _timer += Time.deltaTime; // Óâåëè÷èâàåì òàéìåð íà âðåìÿ, ïðîøåäøåå ñ ïîñëåäíåãî êàäðà

        if (_timer >= 1f)
        {
            _timer = 0f; // Ñáðàñûâàåì òàéìåð
            hp += _regeneration;
        }

        if(hp <= 0)
        { 
            Destroy(gameObject); //Íå çàáûòü äîáàâèòü ñêðèí GAME OVER!!!
        }
    }

    //Ôóíêöèþ íàäî äîðàáîòàòü, êîãäà áóäåò äîáàâëåíà ïåðåìåííàÿ âðåìåíè (æèçíè)
    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
    //TODO
    //ñäåëàòü ôóíêöèþ äëÿ ðåãåíåðàöèè
}