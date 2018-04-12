using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Vector2 _intendedMovement;

    [HideInInspector] public Goblin ControlledGoblin;
    [HideInInspector] public Goblin.GoblinType GoblinType;

    private void Update()
    {
        //get player 1 input and control gobo

        _intendedMovement = Vector2.right * Input.GetAxis("Player2Horizontal") +
                            Vector2.up * Input.GetAxis("Player2Vertical");


        if (!GlobalScript.Paused)
        {
            if (Input.GetButtonDown("Player2AttackStandard"))
                ControlledGoblin.AttackStandard();
            if (Input.GetButtonDown("Player2AttackRange"))
                ControlledGoblin.AttackRange();
            if (Input.GetButtonDown("Player2AttackSupport"))
                ControlledGoblin.AttackSupport();
            if (Input.GetButtonDown("Player2AttackUltimate"))
                ControlledGoblin.AttackUltimate();
        }

        if (ControlledGoblin != null)
            ControlledGoblin.RightStick = Vector2.right * Input.GetAxis("Player2RightStickHor") +
                                          Vector2.down * Input.GetAxis("Player2RightStickVert");
    }

    private void FixedUpdate()
    {
        if (ControlledGoblin != null)
            ControlledGoblin.Rigid.velocity =
                _intendedMovement.normalized * ControlledGoblin.Speed * PlayerController.SpeedMultiplier;
    }
}