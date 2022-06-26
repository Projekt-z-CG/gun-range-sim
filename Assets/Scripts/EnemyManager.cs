using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* Enemy manager for zombie game mode
**/
public class EnemyManager : MonoBehaviour
{
    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewEnemy();
    }
    // When a enemy is killed spawn a new enemy
    private void OnEnable()
    {
        Health.OnEnemyKilled += SpawnNewEnemy;
    }

    // Spawn new enemy in one of the spawn points
    void SpawnNewEnemy()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(1f, m_SpawnPoints.Length-1));

        Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);
    }
}
