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
            if (Input.GetAxis("Player1AttackStandard") > 0.5)
                ControlledGoblin.AttackStandard();
            if (Input.GetAxis("Player1AttackRange") > 0.5)
                ControlledGoblin.AttackRange();
            if (Input.GetAxis("Player1AttackSupport") > 0.5)
                ControlledGoblin.AttackSupport();
            if (Input.GetAxis("Player1AttackUltimate") > 0.5)
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