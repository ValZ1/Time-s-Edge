using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BossRoom : Room
{

    public List<GameObject> RoomOpen;

    private bool IsRoomOpen = false;
    private int Waves;
    private int _index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentGameObject = transform.parent.gameObject;
        _roomActive = true;
        CloseDoors();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRoomOpen)
        {
            if (IsAllRoomInactive())
            {
                OpenDoors();
                IsRoomOpen = true;
            }
        }
    }
    bool IsAllRoomInactive()
    {
        foreach (GameObject room in RoomOpen)
        {
            if (room != null && room.tag == "Room")
            {
                return false;
            }
        }
        return true;
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