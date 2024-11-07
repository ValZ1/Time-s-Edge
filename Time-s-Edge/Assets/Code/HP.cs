using TMPro;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int Hp;

    public Player Player;
    public TextMeshProUGUI Text;

    // Update is called once per frame
    void Update()
    {
        Hp = Player.hp;
        Text.text = Hp.ToString();
    }
}
