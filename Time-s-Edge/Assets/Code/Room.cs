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
    public int cleared_rooms = 0;

    public bool isCurrentRoom = false;
    public bool _roomActive;
    private bool cleared = false;
    void Start()
    {
        if (!is_bossroom)
        {
            _roomActive = true;
            Waves = 1;
            OpenDoors();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cleared_rooms == 7)
        {
            _roomActive = true;
            Waves = UnityEngine.Random.Range(2, 4);
            OpenDoors();
            cleared_rooms++;
        }
    }
    private void OnEnemySpawnPoint()
    {
        if (is_bossroom)
        {
            Instantiate(prefabBoss, BossSpawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            foreach (EnemySpawnPoint enemySpawnPoint in EnemySpawnPoints)
            {
                _index = UnityEngine.Random.Range(0, 5);
                enemySpawnPoint.SpawnEnemy(_index);
                _countEnemy++;
            }
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
        if (cleared)
        {
            GameObject bossRoom = GameObject.Find("RoomTriggerBosS");
            if (bossRoom != null)
            {
                // Достаём ваш скрипт Room с этого объекта
                Room roomScript = bossRoom.GetComponent<Room>();
                if (roomScript != null)
                {
                    // Увеличиваем счётчик
                    roomScript.cleared_rooms++;
                }
                else
                {
                    Debug.LogError("На объекте 'RoomTriggerBosS' нет компонента Room!");
                }
            }
            else
            {
                Debug.LogError("Не найден объект 'RoomTriggerBosS' в сцене!");
            }
            Destroy(gameObject);
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
                //Тут можно реализовать волны
                cleared = true;
                OpenDoors();
            }
        }
        else if (collision.tag == "Player") return;
    }
}
