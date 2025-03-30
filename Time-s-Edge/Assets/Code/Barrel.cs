using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject HealtPotion;
    public int Hp = 2;
    public AudioSource audioData;
    public AudioClip[] sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // ���������, ���� �� � ���� ������ �� ������
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

            audioData.clip = sound[Random.Range(0, 2)];
            audioData.Play();

            Destroy(gameObject);
        }
        Destroy(collision.gameObject);
    }
}
