using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float speed = 0.15f;

    private Rigidbody2D rb;
    private Vector2 moveVector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        moveVector = new Vector2(horiz, vert);
        if (moveVector.magnitude > 1)
        {
            moveVector.Normalize();
        }
        rb.MovePosition(rb.position + moveVector * speed);
    }
}