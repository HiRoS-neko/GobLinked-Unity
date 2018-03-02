using UnityEngine;


public class Player1 : MonoBehaviour
{
    [HideInInspector]
    public Goblin ControlledGoblin;

    private Rigidbody2D _goblinRigidBody;

    private Vector2 _intendedMovement;

    private void Start()
    {
        UpdateGoblinReference();
    }

    private void Update()
    {
        //get player 1 input and control gobo
        
        _intendedMovement = Vector2.right*Input.GetAxis("Player1Horizontal") + Vector2.up*Input.GetAxis("Player1Vertical");
    }

    private void FixedUpdate()
    {
        _goblinRigidBody.velocity = _intendedMovement*ControlledGoblin.Speed;
    }

    public void UpdateGoblinReference()
    {
        _goblinRigidBody = ControlledGoblin.gameObject.GetComponent<Rigidbody2D>();
    }
}