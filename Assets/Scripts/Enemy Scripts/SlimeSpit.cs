﻿using UnityEngine;
/// <summary>
/// Works for any given projectile
/// </summary>
public class SlimeSpit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Goblin")
        {
            other.gameObject.GetComponentInChildren<Goblin>().TakeDamage(2);
            Destroy(gameObject);
        }
    }
}