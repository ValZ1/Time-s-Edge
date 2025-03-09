using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private bool _isMenuActive = false;
    void Start()
    {
        _menu.SetActive(_isMenuActive);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            _isMenuActive = !_isMenuActive;
            _menu.SetActive(_isMenuActive);
            //Time.timeScale = 0f;
        }
    }
}
