using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    public float MaxTurnSpeed = 180;
    public float SmoothTime = 0.1f;

    private float _angle;
    private float _currentVelocity;
    private float _targetAngle;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        _targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        _angle = Mathf.SmoothDampAngle(_angle, _targetAngle, ref _currentVelocity, SmoothTime, MaxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, _angle);
    }
    public float get_targetAngle() { return _targetAngle; }
}
