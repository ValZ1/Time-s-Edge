using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{ 
    public List<Door> Doors;
    public List<EnemySpawnPoint> EnemySpawnPoints;

    private int Waves;
    private int _index;
    private int _countEnemy = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Waves = Random.Range(2, 4);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnemySpawnPoint()
    {
        foreach (EnemySpawnPoint enemySpawnPoint in EnemySpawnPoints)
        {
            _index = Random.Range(0, 5);
            enemySpawnPoint.SpawnEnemy(_index);
            _countEnemy++;
        }
    }
    private void CloseDoors()
    {
        foreach (Door door in Doors)
        {
            door.Close();
        }
    }
    private void OpenDoors()
    {
        foreach (Door door in Doors)
        {
            door.Open();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)){
            CloseDoors();
            OnEnemySpawnPoint();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _countEnemy--;
            if (_countEnemy == 0)
            {
                //Тут можно реализовать волны
                OpenDoors();
            }
        }
        else if (collision.tag == "Player") gameObject.SetActive(false);
    }
}
