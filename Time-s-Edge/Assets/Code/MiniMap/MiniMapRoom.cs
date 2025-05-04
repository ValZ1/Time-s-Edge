using UnityEngine;

public class MiniMapRoom : MonoBehaviour
{
    public Vector2 GetRoomSize()
    {
        Collider2D roomCollider = GetComponent<Collider2D>();
        if (roomCollider != null)
        {
            if (roomCollider is BoxCollider2D)
            {
                return ((BoxCollider2D)roomCollider).size;
            }
        }
        return Vector2.one;
    }
    public Vector3 GetColliderCenterWorldPosition()
    {
        Collider2D roomCollider = GetComponent<Collider2D>();
        if (roomCollider != null)
        {
            return roomCollider.bounds.center;
        }
        return transform.position;
    }
}
