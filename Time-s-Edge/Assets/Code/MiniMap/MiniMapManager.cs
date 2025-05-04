using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MiniMapManager : MonoBehaviour
{
    public Camera miniMapCamera;
    public RawImage miniMapRawImage;

    public GameObject startIconPrefab;
    public GameObject roomIconPrefab;
    public GameObject clearedRoomIconPrefab;
    public GameObject passageIconPrefab;
    public GameObject shopIconPrefab;
    public GameObject bossRoomIconPrefab;
    public GameObject playerIconPrefab;

    public Transform playerTransform;
    
    public string startTag = "start_room";
    public string roomTag = "Room";
    public string passageTag = "Passage";
    public string shopTag = "Shop";
    public string bossRoomTag = "BossRoom";

    private List<GameObject> roomIcons = new List<GameObject>();
    private List<GameObject> passageIcons = new List<GameObject>();
    private List<GameObject> shopIcons = new List<GameObject>();
    private List<GameObject> bossRoomIcons = new List<GameObject>();

    private GameObject playerIcon;

    void Start()
    {
        if (miniMapCamera == null || miniMapRawImage == null || playerTransform == null)
        {
            enabled = false;
            return;
        }

        GenerateMiniMap();
    }

    void GenerateMiniMap()
    {
        GameObject[] start = GameObject.FindGameObjectsWithTag(startTag);
        GameObject[] rooms = GameObject.FindGameObjectsWithTag(roomTag);
        GameObject[] passages = GameObject.FindGameObjectsWithTag(passageTag);
        GameObject[] shops = GameObject.FindGameObjectsWithTag(shopTag);
        GameObject[] bossRooms = GameObject.FindGameObjectsWithTag(bossRoomTag);

        foreach (GameObject _start in start)
        {
            CreateIcon(_start, startIconPrefab, roomIcons);
        }

        foreach (GameObject room in rooms)
        {
            CreateIcon(room, roomIconPrefab, roomIcons);
        }

        foreach (GameObject passage in passages)
        {
            CreateIcon(passage, passageIconPrefab, passageIcons);
        }

        foreach (GameObject shop in shops)
        {
            CreateIcon(shop, shopIconPrefab, shopIcons);
        }

        foreach (GameObject bossRoom in bossRooms)
        {
            CreateIcon(bossRoom, bossRoomIconPrefab, bossRoomIcons);
        }

        CreatePlayerIcon();
    }
    void CreateIcon(GameObject target, GameObject prefab, List<GameObject> iconList)
    {
        GameObject icon = Instantiate(prefab, miniMapRawImage.transform);
        RoomIcon link = icon.GetComponent<RoomIcon>();
        if (link == null)
        {
            link = icon.AddComponent<RoomIcon>();
        }
        link.room = target;
        iconList.Add(icon);
        UpdateIconPosition(icon, target);


        MiniMapRoom roomScript = target.GetComponentInChildren<MiniMapRoom>();
        if (roomScript != null)
        {
            Vector2 roomSize = roomScript.GetRoomSize();
            RectTransform rawImageRectTransform = miniMapRawImage.GetComponent<RectTransform>();

            Vector3 roomBottomLeftWorld = target.transform.position - new Vector3(roomSize.x / 2, roomSize.y / 2, 0);
            Vector3 roomTopRightWorld = target.transform.position + new Vector3(roomSize.x / 2, roomSize.y / 2, 0);

            Vector3 roomBottomLeftViewport = miniMapCamera.WorldToViewportPoint(roomBottomLeftWorld);
            Vector3 roomTopRightViewport = miniMapCamera.WorldToViewportPoint(roomTopRightWorld);

            float iconWidth = (roomTopRightViewport.x - roomBottomLeftViewport.x) * rawImageRectTransform.rect.width;
            float iconHeight = (roomTopRightViewport.y - roomBottomLeftViewport.y) * rawImageRectTransform.rect.height;

            icon.GetComponent<RectTransform>().sizeDelta = new Vector2(iconWidth, iconHeight);
        }
    }

    void CreatePlayerIcon()
    {
        playerIcon = Instantiate(playerIconPrefab, miniMapRawImage.transform);
        UpdateIconPosition(playerIcon, playerTransform.gameObject);
    }


    void UpdateIconPosition(GameObject icon, GameObject target)
    {
        RectTransform rawImageRectTransform = miniMapRawImage.GetComponent<RectTransform>();
        MiniMapRoom roomScript = target.GetComponentInChildren<MiniMapRoom>();

        Vector3 roomSize = Vector3.zero;
        Vector3 roomCenterWorld;

        if (roomScript != null)
        {
            roomSize = roomScript.GetRoomSize();
            roomCenterWorld = roomScript.GetColliderCenterWorldPosition();
        }
        else
        {
            roomCenterWorld = target.transform.position;
        }

        Vector3 roomCenterViewport = miniMapCamera.WorldToViewportPoint(roomCenterWorld);
        float xOffset = (roomCenterViewport.x - 0.5f) * rawImageRectTransform.rect.width;
        float yOffset = (roomCenterViewport.y - 0.5f) * rawImageRectTransform.rect.height;

        icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xOffset, yOffset);
    }

    void UpdateIconPosition(GameObject icon, Transform target)
    {
        RectTransform rawImageRectTransform = miniMapRawImage.GetComponent<RectTransform>();
        Vector3 worldPosition = target.position;
        Vector3 viewportPosition = miniMapCamera.WorldToViewportPoint(worldPosition);
        icon.GetComponent<RectTransform>().anchoredPosition = new Vector2((viewportPosition.x - 0.5f) * rawImageRectTransform.rect.width, (viewportPosition.y - 0.5f) * rawImageRectTransform.rect.height);
    }

    void Update()
    {
        UpdateMiniMap();
    }


    void UpdateMiniMap()
    {
        UpdateIcons(roomIcons);
        UpdateIcons(passageIcons);
        UpdateIcons(shopIcons);
        UpdateIcons(bossRoomIcons);

        if (playerIcon != null)
        {
            UpdateIconPosition(playerIcon, playerTransform);
            playerIcon.GetComponent<RectTransform>().SetAsLastSibling();
        }
    }

    void UpdateIcons(List<GameObject> iconList)
    {
        List<GameObject> iconsToProcess = new List<GameObject>(iconList);

        foreach (GameObject icon in iconsToProcess)
        {
            if (icon != null)
            {
                RoomIcon link = icon.GetComponent<RoomIcon>();
                if (link != null && link.room != null)
                {
                    if (link.room.activeInHierarchy)
                    {
                        UpdateIconPosition(icon, link.room);
                    }
                    else
                    {
                        iconList.Remove(icon);
                        Destroy(icon);
                    }
                }
                else
                {
                    iconList.Remove(icon);
                    Destroy(icon);
                }
            }
            else
            {
                iconList.Remove(icon);
            }
        }
    }

    public void MarkRoomAsCleared(GameObject room)
    {
        GameObject iconToChange = null;
        foreach (GameObject icon in roomIcons)
        {
            if (icon != null)
            {
                RoomIcon link = icon.GetComponent<RoomIcon>();
                if (link != null && link.room == room)
                {
                    iconToChange = icon;
                    break;
                }
            }
        }

        if (iconToChange != null)
        {
            Vector2 iconPosition = iconToChange.GetComponent<RectTransform>().anchoredPosition;
            // Vector2 iconSize = iconToChange.GetComponent<RectTransform>().sizeDelta;
            roomIcons.Remove(iconToChange);
            Destroy(iconToChange);

            GameObject newIcon = Instantiate(clearedRoomIconPrefab, miniMapRawImage.transform);
            RectTransform newIconRectTransform = newIcon.GetComponent<RectTransform>();
            newIconRectTransform.anchoredPosition = iconPosition;

            roomIcons.Add(newIcon);

            RoomIcon link = newIcon.GetComponent<RoomIcon>();
            if (link == null)
            {
                link = newIcon.AddComponent<RoomIcon>();
            }
            link.room = room;

            MiniMapRoom roomScript = room.GetComponentInChildren<MiniMapRoom>();
            if (roomScript != null)
            {
                Vector2 roomSize = roomScript.GetRoomSize();
                RectTransform rawImageRectTransform = miniMapRawImage.GetComponent<RectTransform>();

                Vector3 roomBottomLeftWorld = room.transform.position - new Vector3(roomSize.x / 2, roomSize.y / 2, 0);
                Vector3 roomTopRightWorld = room.transform.position + new Vector3(roomSize.x / 2, roomSize.y / 2, 0);

                Vector3 roomBottomLeftViewport = miniMapCamera.WorldToViewportPoint(roomBottomLeftWorld);
                Vector3 roomTopRightViewport = miniMapCamera.WorldToViewportPoint(roomTopRightWorld);

                float iconWidth = (roomTopRightViewport.x - roomBottomLeftViewport.x) * rawImageRectTransform.rect.width;
                float iconHeight = (roomTopRightViewport.y - roomBottomLeftViewport.y) * rawImageRectTransform.rect.height;

                newIconRectTransform.sizeDelta = new Vector2(iconWidth, iconHeight);
            }

            UpdateIconPosition(newIcon, room);
        }
    }
}