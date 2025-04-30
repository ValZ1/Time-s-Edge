using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected TextMeshProUGUI textBox;
    private bool _isPlayerInTrigger = false;
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
        void Update()
    {
        //Debug.Log(player.get_CurHp());
        if (_isPlayerInTrigger)
        { 
                if (Input.GetKeyDown(KeyCode.E))
                {

                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            _isPlayerInTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
        }
    }
}
