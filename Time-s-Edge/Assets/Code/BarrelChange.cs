using UnityEngine;

public class BarrelChange : MonoBehaviour
{
    public GameObject HealthPotion;
    public float DropChance = 20f;
    public int Hp = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null && bullet.player != null)
        {
            Hp -= bullet.player.get_Damage();
        }

        if (Hp <= 0)
        {
            if (HealthPotion != null && Random.Range(0f, 100f) < DropChance)
            {
                Instantiate(HealthPotion, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }

        Destroy(collision.gameObject);
    }
}
