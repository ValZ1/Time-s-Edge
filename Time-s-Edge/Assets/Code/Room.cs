using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Room : MonoBehaviour
{ 
    public List<Door> Doors;
    public List<EnemySpawnPoint> EnemySpawnPoints;
    public EnemySpawnPoint BossSpawnPoint;
    public EnemyBoss prefabBoss;


    private int Waves;
    private int _index;
    private int _countEnemy = 0;
    public bool is_bossroom = false;

    public bool isCurrentRoom = false;
    public bool _roomActive;
    void Start()
    {
        _roomActive = true;
        Waves = UnityEngine.Random.Range(2, 4);
        OpenDoors();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnemySpawnPoint()
    {
        foreach (EnemySpawnPoint enemySpawnPoint in EnemySpawnPoints)
        {
            _index = UnityEngine.Random.Range(0, 5);
            enemySpawnPoint.SpawnEnemy(_index);
            _countEnemy++;
        }
        if (is_bossroom)
        {
            Instantiate(prefabBoss, BossSpawnPoint.transform.position, Quaternion.identity);
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
        if (_roomActive)
        {
            if (collision.TryGetComponent(out Player player))
            {
                CloseDoors();
                OnEnemySpawnPoint();
                GameController gameController = FindAnyObjectByType<GameController>();
                if (gameController != null) gameController.SetCurrentRoom(this);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _countEnemy--;
            if (_countEnemy == 0)
            {
                _roomActive = false;
                //��� ����� ����������� �����
                OpenDoors();
            }
        }
        else if (collision.tag == "Player") return;
    }
}
