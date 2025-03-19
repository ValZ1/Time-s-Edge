using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerArm;
    private bool _isMenuActive = false;
    void Start()
    {
        Time.timeScale = 1f;
        _menu.SetActive(_isMenuActive);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MenuControl();
        }
    }
    private void MenuControl()
    {
        _isMenuActive = !_isMenuActive;
        _menu.SetActive(_isMenuActive);
        Time.timeScale = _isMenuActive ? 0f : 1f;
        _player.enabled = !_isMenuActive;
        _playerArm.SetActive(!_isMenuActive);
    }
    public void Save()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        MenuControl();
    }
}
