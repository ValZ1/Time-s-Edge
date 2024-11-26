using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    public GameObject PrefabBullet;
    public Transform ArmCenter;

    private float _cooldownTime = 0.5f;
    void Update()
    {
        //Стрельба
        if (Input.GetMouseButton(0) && _cooldownTime >=0.5f)
        {
            _cooldownTime = 0;
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
        }

        if (Input.GetKey(KeyCode.Space) && _cooldownTime >= 0.5f)
        {
            _cooldownTime = 0;
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
        }
        _cooldownTime += Time.deltaTime;
    }
}
