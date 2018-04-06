using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Vector2 _intendedMovement;

    [HideInInspector] public Goblin ControlledGoblin;

    private void Update()
    {
        //get player 1 input and control gobo

        _intendedMovement = Vector2.right * Input.GetAxis("Player1Horizontal") +
                            Vector2.up * Input.GetAxis("Player1Vertical");
        
        
        if (Input.GetButtonDown("Player1AttackStandard"))
            ControlledGoblin.AttackStandard();
        if (Input.GetButtonDown("Player1AttackRange"))
            ControlledGoblin.AttackRange();
        if (Input.GetButtonDown("Player1AttackSupport"))
            ControlledGoblin.AttackSupport();
        if (Input.GetButtonDown("Player1AttackUltimate"))
            ControlledGoblin.AttackUltimate();
    }

    private void FixedUpdate()
    {
        ControlledGoblin.Rigid.velocity =
            _intendedMovement.normalized * ControlledGoblin.Speed * PlayerController.SpeedMultiplier;
    }
}