using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Vector2 _intendedMovement;

    [HideInInspector] public Goblin ControlledGoblin;
    [HideInInspector] public Goblin.GoblinType GoblinType;

    private void Update()
    {
        //get player 1 input and control gobo

        _intendedMovement = Vector2.right * Input.GetAxis("Player1Horizontal") +
                            Vector2.up * Input.GetAxis("Player1Vertical");

        if (!GlobalScript.Paused)
        {
            if (Input.GetButtonDown("Player1AttackStandard"))
                ControlledGoblin.AttackStandard();
            if (Input.GetButtonDown("Player1AttackRange"))
                ControlledGoblin.AttackRange();
            if (Input.GetButtonDown("Player1AttackSupport"))
                ControlledGoblin.AttackSupport();
            if (Input.GetButtonDown("Player1AttackUltimate"))
                ControlledGoblin.AttackUltimate();
        }

        ControlledGoblin.RightStick = Vector2.right * Input.GetAxis("Player1RightStickHor") +
                                      Vector2.down * Input.GetAxis("Player1RightStickVert");
    }

    private void FixedUpdate()
    {
        ControlledGoblin.Rigid.velocity =
            _intendedMovement.normalized * ControlledGoblin.Speed * PlayerController.SpeedMultiplier;
    }
}