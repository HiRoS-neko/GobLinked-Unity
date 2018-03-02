using UnityEngine;


public class Player2 : MonoBehaviour
{
    [HideInInspector]
    public Goblin ControlledGoblin;

    private Rigidbody2D _goblinRigidBody;

    private Vector2 _intendedMovement;
    
    public void UpdateGoblinReference()
    {
        _goblinRigidBody = ControlledGoblin.gameObject.GetComponent<Rigidbody2D>();
    }
}