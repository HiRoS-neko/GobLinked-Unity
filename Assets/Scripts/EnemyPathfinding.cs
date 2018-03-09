using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    private bool[,] movableTiles;                //
    public float visibleRange;                   //
    private IEnumerator goHitArray;              //
    private Collider2D[] hitObjects;             //
    public bool search = false;                  //
    public Rigidbody2D body;                     //
    [Range(0, 100)] public float speed;          //

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
                body.velocity = (((Vector2)hitObjects[i].transform.position - body.position)).normalized*speed*Time.fixedDeltaTime;
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