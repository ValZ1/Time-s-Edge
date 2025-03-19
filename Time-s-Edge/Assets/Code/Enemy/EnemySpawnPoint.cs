using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<GameObject> PrefabEnemys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnEnemy(int index)
    {
        Instantiate(PrefabEnemys[index], transform.position, Quaternion.identity);
    }
}
