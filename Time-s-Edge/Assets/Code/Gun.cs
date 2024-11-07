using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject PrefabBullet;
    public Transform ArmCenter;

    private float _cooldownTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Стрельба
        if (Input.GetMouseButtonDown(0) && _cooldownTime >=0.5f)
        {
            _cooldownTime = 0;
            Instantiate(PrefabBullet, transform.position, ArmCenter.rotation);
        }
        _cooldownTime += Time.deltaTime;
    }
}
