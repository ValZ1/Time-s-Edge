using UnityEngine;

public class ArmPlayer : MonoBehaviour
{
    public float MaxTurnSpeed = 180;
    public float SmoothTime = 0.1f;

    private float _angle;
    private float _currentVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        _angle = Mathf.SmoothDampAngle(_angle, targetAngle, ref _currentVelocity, SmoothTime, MaxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, _angle);
    }
}
