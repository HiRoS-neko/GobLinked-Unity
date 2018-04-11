using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelChange : MonoBehaviour
{
    [SerializeField] private string _level;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(_level);
    }
}