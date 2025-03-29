using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Можно потом заменить SetActive на проигрывание анимации и вкл/откл коллайдера объекта
    public void Open()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        gameObject.SetActive(true);
    }
}
