using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Prefab;
    public Transform ArmCenter;

    private GameObject _instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Стрельба
        if (Input.GetMouseButtonDown(0))
        {
            _instance = Instantiate(Prefab, transform.position, ArmCenter.rotation);
        }
    }
}
