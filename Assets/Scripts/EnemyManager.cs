using System.Collections;
using System.Collections.Generic; // Important pentru List<>
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Settings")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] Transform enemiesParent;

    float currentTimeBetweenSpawns;

    // LISTA NOUĂ: Ținem minte toți inamicii vii
    List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (enemiesParent == null)
        {
            GameObject parentObj = GameObject.Find("Enemies");
            if (parentObj != null)
            {
                enemiesParent = parentObj.transform;
            }
        }
    }

    private void Update()
    {
        // MODIFICARE 1: Verificăm dacă WaveManager ne lasă să spawnăm
        if (WaveManager.instance.waveRunning == false)
        {
            return; // Dacă valul s-a oprit, nu mai facem nimic
        }

        currentTimeBetweenSpawns -= Time.deltaTime;

        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    void SpawnEnemy()
    {
        float randomX = UnityEngine.Random.Range(-16f, 16f);
        float randomY = UnityEngine.Random.Range(-8f, 8f);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // MODIFICARE 2: Adăugăm inamicul în listă ca să știm de el
        enemies.Add(newEnemy);

        if (enemiesParent != null)
        {
            newEnemy.transform.SetParent(enemiesParent);
        }
    }

    // MODIFICARE 3: Funcția care distruge tot (apelată de WaveManager)
    public void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        // Golim lista după ce i-am distrus pe toți
        enemies.Clear();
    }
}