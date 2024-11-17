using UnityEngine;

public class healthpotion: MonoBehaviour
{
    public int heal = -50;
    void Start()
    {
    }
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(heal);
            Destroy(gameObject);
        }
    }
}