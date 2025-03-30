using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public List<Room> rooms;
    public GameObject portalPrefab;

    private GameObject _portalInstance;
    private void Start()
    {

        rooms = new List<Room>(FindObjectsByType<Room>(FindObjectsSortMode.None));
    }

    void Update()
    {
        if (AreAllRoomsCleared())
        {
            Room lastRoom = FindLastRoom();

            if (lastRoom != null && _portalInstance == null)
            {
                SpawnPortalInRoomCenter(lastRoom);
            }
        }
    }

    bool AreAllRoomsCleared()
    {
        return rooms.All(room => room._roomActive == false);
    }

    // Функция для определения последней комнаты
    private Room FindLastRoom()
    {
        foreach (Room room in rooms)
        {
            if (room.isCurrentRoom) return room;
        }
        return null;
    }
    public void SetCurrentRoom(Room newCurrentRoom)
    {
        foreach (Room room in rooms)
        {
            room.isCurrentRoom = false;
        }
        newCurrentRoom.isCurrentRoom = true;
    }
    private void SpawnPortalInRoomCenter(Room room)
    {
            Bounds roomBounds = room.GetComponent<Collider2D>().bounds;

            Vector3 portalPosition = roomBounds.center;

            _portalInstance = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
    }
}