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
    public GameObject parentGameObject;

    private int Waves;
    private int _index;
    public int _countEnemy = 0;
    public bool is_bossroom = false;

    public bool isCurrentRoom = false;
    public bool _roomActive;
    void Start()
    {
        parentGameObject = transform.parent.gameObject;
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
            _countEnemy++;
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
    if (collision.tag == "Enemy" || collision.tag == "Boss")
    {
        _countEnemy--;
            Debug.Log("ds");
        if (_countEnemy == 0)
        {
            _roomActive = false;
            OpenDoors();

            MiniMapManager miniMapManager = FindObjectOfType<MiniMapManager>();
            if (miniMapManager != null)
            {
                miniMapManager.MarkRoomAsCleared(parentGameObject);
            }

                parentGameObject.tag = "Passage";
        }
    }
    else if (collision.tag == "Player") return;
}
}
