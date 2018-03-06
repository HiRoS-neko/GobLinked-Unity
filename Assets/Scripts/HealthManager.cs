using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] [Range(0, 16)] private int _currentHealth = 5;
    [SerializeField] private List<Heart> _hearts;


    [SerializeField] [Range(0, 16)] private int _maxHealth = 5;

    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
    }


    // Use this for initialization

    private void FixedUpdate()
    {
        if (_maxHealth > _hearts.Count)
            _maxHealth = _hearts.Count;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        for (var i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].Active(i < _maxHealth);
            _hearts[i].Fill(i < _currentHealth);
        }
    }


    public void SetMaxHealth(int health)
    {
        _maxHealth = health;
        if (_currentHealth == 0) _currentHealth = _maxHealth;
    }

    public void SetHealth(int num)
    {
        if (num <= _hearts.Count)
            _currentHealth = num;
    }
}