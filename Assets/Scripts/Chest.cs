using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var item in _items)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        _items = new List<Item>();
    }
}