using UnityEngine;

public class Krilk : Goblin
{
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }
}