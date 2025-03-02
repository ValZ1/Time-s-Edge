using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject HealtPotion;
    public int Hp = 2;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // ѕровер€ем, есть ли у пули ссылка на игрока
            if (bullet.player != null)
            {
                Hp -= bullet.player.get_Damage();
            }
        }
            //Hp -= player.get_Damage();
        if (Hp <= 0)
        {
            if(HealtPotion != null)
            {
                Instantiate(HealtPotion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        Destroy(collision.gameObject);
    }
}
