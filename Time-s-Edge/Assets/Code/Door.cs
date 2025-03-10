using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Start()
    {
        Open();
    }
    //����� ����� �������� SetActive �� ������������ �������� � ���/���� ���������� �������
    public void Open()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        gameObject.SetActive(true);
    }
}
