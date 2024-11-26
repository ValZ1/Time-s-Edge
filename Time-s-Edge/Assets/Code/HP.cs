using TMPro;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int Hp;
    public int Damage;
    public float spdamage;

    public Player Player;
    public Bullet Bullet;
    public TextMeshProUGUI Text;

    // Update is called once per frame
    void Update()
    {
        Damage = Bullet.DamageBullet;
        spdamage = Bullet.SpeedBullet;
        Hp = Player.get_CurHp();
        Text.text = "HP " + Hp.ToString() + "\n" +"Damage "+ Damage.ToString()+"\n"+"Atakspped "+spdamage.ToString();
    }
}
