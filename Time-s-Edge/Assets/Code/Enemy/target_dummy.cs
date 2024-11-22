using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class target_dummy : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI Text;
    private Rigidbody2D _rb;
    private int _last_Taken_damage = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Text.text = _last_Taken_damage.ToString();
    }
    public void TakeDamage(int damage)
    {
        _last_Taken_damage = damage;
        player.TakeDamage(-10);
    }
}