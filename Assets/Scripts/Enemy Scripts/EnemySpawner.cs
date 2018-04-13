using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Prefabs of enemies")]
    public GameObject[] enemiesToSpawn;

    [Tooltip("The weight of each prefab to actually spawn")]
    public int[] enemyWeights;
    
    [Tooltip("Time between respawns")] 
    public int spawnTimer;

    private float currentTimer;

    private int check;
    
    private void Update()
    {
        if (currentTimer >= spawnTimer)
        {
            check = Random.Range(0, 1);
            
        }
    }
}