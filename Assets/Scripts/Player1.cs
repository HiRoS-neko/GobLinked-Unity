using UnityEngine;


public class Player1 : MonoBehaviour
{
    [SerializeField]private Goblin _controlledGoblin;

    private Rigidbody2D _goblinRigidBody;

    private Vector2 _intendedMovement;
    
    private void Start()
    {
        //just in case someone is lazy,,, please make sure not to release with unassigned references
        if (_controlledGoblin == null)
        {
            _controlledGoblin = FindObjectOfType<Goblin>();
        }
    }


    private void Update()
    {
        //get player 1 input and control gobo
        
        _intendedMovement = Vector2.right*Input.GetAxis("Player1Horzontal") + Vector2.up*Input.GetAxis("Player1Horizontal");
    }

    private void FixedUpdate()
    {
        _goblinRigidBody.AddForce(_intendedMovement*_controlledGoblin.Speed, ForceMode2D.Force);
    }
}