using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoDisplay : MonoBehaviour
{
    public static ItemInfoDisplay Instance;

    [Header("UI References")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] public TMP_Text descriptionText;
    [SerializeField] public TMP_Text parametersText;
    [SerializeField] public TMP_Text loreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            HideInfo(); // �������� ��� ������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowItemInfo(ItemFather item)
    {
        if (item == null) return;

        // ��������� �����
        descriptionText.text = item.discriprion.ToString();
        parametersText.text = item.parameters;
        loreText.text = item.lore;

        // ���������� ������
        infoPanel.SetActive(true);

        Debug.Log($"Displaying: {item.discriprion}");
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        Debug.Log("Info panel hidden");
    }
}