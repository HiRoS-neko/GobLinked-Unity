using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    public AudioSource source;
    public Animator animate;
    private bool isTriggered = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered)
        {
            if (other.CompareTag("Goblin"))
            {
                foreach (var item in _items) Instantiate(item, transform.position, Quaternion.identity);
                source.Play();
                animate.SetTrigger("open");
                _items = new List<Item>();
                isTriggered = true;
            }
        }
    }
}