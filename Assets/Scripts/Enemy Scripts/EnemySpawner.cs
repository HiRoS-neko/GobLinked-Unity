using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Prefabs of enemies")]
    public GameObject[] enemiesToSpawn;

    [Tooltip("The weight of each prefab to actually spawn")]
    public int[] enemyWeights;
    
    [Tooltip("Time between respawns")] 
    public int spawnTimer;
    
    private float currentTimer;    //Current time elapsed
    
    private int check;    //Check number for the weights
    
    private void Update()
    {
        if (currentTimer >= spawnTimer)    //Checking if it is time to spawn
        {
            check = Random.Range(0, 1);    //Random chekc number
            for (int i = 0; i < enemiesToSpawn.Length; i++)    //Checking every spawnable prefab
            {
                if (check == 0)    //Checking if the digit falls inside the range of a prefab
                {
                    Instantiate(enemiesToSpawn[i]);    //
                }
            }
        }
    }
}