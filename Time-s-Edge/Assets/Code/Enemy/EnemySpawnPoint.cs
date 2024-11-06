using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject PrefabEnemy;
    public float CooldownSpawn = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (CooldownSpawn >= 5.0f)
        {
            Instantiate(PrefabEnemy, transform.position, Quaternion.identity);
            CooldownSpawn = 0.0f;
        }
        CooldownSpawn += Time.deltaTime;
    }
}
