using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDummy : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI Text;

    private Rigidbody2D _rb;
    private int _lastTakenDamage = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Text.text = _lastTakenDamage.ToString();
    }
    public void TakeDamage(int damage)
    {
        _lastTakenDamage = damage;
        player.TakeDamage(-10);
    }
}