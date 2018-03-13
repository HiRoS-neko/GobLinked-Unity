using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private bool[,] movableTiles;      
    
    [Tooltip("")]
    public float visibleRange;       
    
    private IEnumerator goHitArray;  
    
    private Collider2D[] hitObjects; 
    
    public bool search = false;     
    
    public Rigidbody2D body;     
    
    [Range(0, 100)]
    [Tooltip("The speed at which the enemy tracks the player")] 
    public float speed;          

    void Start()
    {
        StartCoroutine(checkHitArray(0.01f));
    }

    void FixedUpdate()
    {
        for (int i = 0; i < hitObjects.Length; i++)
        {
            Debug.Log("FOR loop");
            if (hitObjects[i].tag == "Goblin")
            {
                //body.velocity = (((Vector2)hitObjects[i].transform.position - body.position)).normalized*speed*Time.fixedDeltaTime;
                if (hitObjects[i].Distance(body.GetComponentInParent<Collider2D>()).distance <= visibleRange)
                {
                    //Check for Sector 1 (TL). (-x, +y)
                    if (0 < (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) &&
                        (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) < 90) 
                    {
                        body.transform.position = (Vector2) body.transform.position + new Vector2(-32, 32);
                    }
                    //Check for Sector 2 (BL). (-x, -y)
                    if (90 < (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) &&
                        (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) < 180) 
                    {
                        body.transform.position = (Vector2) body.transform.position + new Vector2(-32, -32);
                    }
                    //Check for Sector 3 (BR). (+x, -y)
                    if (-180 < (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) &&
                        (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) < -90) 
                    {
                        body.transform.position = (Vector2) body.transform.position + new Vector2(32, -32);
                    }
                    //Check for Sector 4 (TR). (+x, +y)
                    if (90 < (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) &&
                        (Vector2.SignedAngle(body.position, hitObjects[i].transform.position)) < 180) 
                    {
                        body.transform.position = (Vector2) body.transform.position + new Vector2(32, 32);
                    }
                }
            }
        }
    }

    private IEnumerator checkHitArray(float waitTime)
    {
        Debug.Log("Coroutine");

        while (true)
        {
            hitObjects = Physics2D.OverlapCircleAll(transform.position, visibleRange);
            yield return new WaitForSeconds(waitTime);
        }
    }
}