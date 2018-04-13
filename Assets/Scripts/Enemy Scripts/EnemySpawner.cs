using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;


    [Tooltip("Time between respawns")] public float spawnTimer;


    private void Update()
    {
    }
}