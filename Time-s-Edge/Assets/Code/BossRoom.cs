using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BossRoom : MonoBehaviour
{
    public List<Door> Doors;
    public List<EnemySpawnPoint> EnemySpawnPoints;
    public List<GameObject> RoomOpen;

    private bool IsRoomOpen = false;
    private float _cooldownTime = 2.0f;
    private int _index;
    private int _countEnemy = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseDoors();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRoomOpen) {
            if (IsAllRoomInactive())
            {
                OpenDoors();
                IsRoomOpen = true;
            }
        }
        _cooldownTime += Time.deltaTime;
    }
    bool IsAllRoomInactive()
    {
        foreach (GameObject room in RoomOpen)
        {
            if (room != null && room.activeInHierarchy)
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
            enemySpawnPoint.SpawnEnemy(0);
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
        if (collision.TryGetComponent(out Player player))
        {
            CloseDoors();
            OnEnemySpawnPoint();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _countEnemy--;
            if (_cooldownTime >=2.0f)
            {
                _cooldownTime = 0f;
                OnEnemySpawnPoint();
            }
        }
        else if (collision.tag == "Player") gameObject.SetActive(false);
    }
}
