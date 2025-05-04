using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _playerArm;

    [SerializeField] private GameObject _miniMap;

    private bool _isMenuActive = false;
    private bool _isMiniMenuActive;
    void Start()
    {
        Time.timeScale = 1f;
        _isMiniMenuActive = true;
        _menu.SetActive(_isMenuActive);
        _loseMenu.SetActive(false);
        _miniMap.SetActive(_isMiniMenuActive);
    }

    void Update()
    {
        if (!_player.isDie)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                MenuControl();
            }
        }
        else
        {
            _miniMap.SetActive(false);
            LoseMenuControl();
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ExitToMenu();
            }
        }
    }
    private void MenuControl()
    {
        _isMiniMenuActive = !_isMiniMenuActive;
        _miniMap.SetActive(_isMiniMenuActive);
        _isMenuActive = !_isMenuActive;
        _menu.SetActive(_isMenuActive);
        Time.timeScale = _isMenuActive ? 0f : 1f;
        _player.enabled = !_isMenuActive;
        _playerArm.SetActive(!_isMenuActive);
    }
    private void LoseMenuControl()
    {
        Time.timeScale = 0f;
        _player.enabled = false;
        _playerArm.SetActive(false);
        _loseMenu.SetActive(true);
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
