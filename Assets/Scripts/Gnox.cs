using UnityEngine;

public class Gnox : Goblin
{
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }
}