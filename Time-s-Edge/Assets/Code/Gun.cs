using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject prefab;
    public Transform arm;

    private GameObject instance;
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
            instance = Instantiate(prefab, transform.position, arm.rotation);
        }
    }
}
