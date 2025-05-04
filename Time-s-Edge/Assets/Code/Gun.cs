using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    
    public GameObject PrefabBullet;
    public Transform ArmCenter;
    public Player player;
    private float _cooldownTime;
    private float max_cooldownTime;

    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        max_cooldownTime = 1 / player.count_atack_in_sec;
    }

     
    public void recalc()
    {
        max_cooldownTime = 1 / player.count_atack_in_sec;
    }

    void Update()
    {

     
        if (Input.GetMouseButton(0) && _cooldownTime >= player.max_cooldownTime)
        {
            _cooldownTime = 0;
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
        }

        if (Input.GetKey(KeyCode.Space) && _cooldownTime >= player.max_cooldownTime)
        {
            _cooldownTime = 0;
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
        }
        _cooldownTime += Time.deltaTime;
    }
}
