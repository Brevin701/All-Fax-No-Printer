using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnermySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesperSecondCap = 15f;
    [SerializeField] private TMP_Text waveCounter;
    [SerializeField] private float maxWave = 2f;


    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawning = false;

    public GameObject levelComplete;
    public TextMeshProUGUI waveCountText;

    
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(StartWave());
    }
    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) 
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        if (currentWave < maxWave)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        else if (currentWave == maxWave) 
        {
            timeBetweenWaves = 0f;
        }
        
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }
    
    private void EndWave() 
    { 
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        UpdateWaveCounter(currentWave);
        StartCoroutine(StartWave());
    }
    private void SpawnEnemy()
    {
        if (currentWave <= maxWave)
        {
            
            if (currentWave > 1)
            {
                int index = Random.Range(0, enemyPrefabs.Length);
                GameObject prefabToSpawn = enemyPrefabs[index];
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            }
            else if (currentWave == 1) 
            {
                GameObject prefabToSpawn = enemyPrefabs[0];
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            }
            
            
        }
        else
        {
            levelComplete.SetActive(true);

        }
        
        
        
    }
  
    private int EnemiesPerWave() 
    {
        int enemies = Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        UpdateWaveCounter(currentWave);
        return enemies;
    }
    // Update is called once per frame
    private float EnemiesPerSecond() 
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesperSecondCap);
    }

    public void UpdateWaveCounter(int currentWave)
    {
        waveCounter.gameObject.SetActive(true);
        if (currentWave < maxWave) 
        {
            waveCounter.text = "Wave: " + currentWave.ToString();
        }
        else if(currentWave == maxWave)
        {
            waveCounter.text = "Wave: " + maxWave.ToString();
        }
        waveCountText.text = "Waves Survived: " + (currentWave - 1).ToString();
    }
    


}
