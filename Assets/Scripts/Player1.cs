using UnityEngine;


public class Player1 : MonoBehaviour
{
    [HideInInspector]
    public Goblin ControlledGoblin;

    private Vector2 _intendedMovement;

    private void Update()
    {
        //get player 1 input and control gobo
        
        _intendedMovement = Vector2.right*Input.GetAxis("Player1Horizontal") + Vector2.up*Input.GetAxis("Player1Vertical");
    }

    private void FixedUpdate()
    {
        ControlledGoblin.Rigid.velocity = _intendedMovement*ControlledGoblin.Speed;
    }
}