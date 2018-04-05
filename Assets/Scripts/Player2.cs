using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Vector2 _intendedMovement;

    [HideInInspector] public Goblin ControlledGoblin;

    private void Update()
    {
        //get player 1 input and control gobo

        _intendedMovement = Vector2.right * Input.GetAxis("Player2Horizontal") +
                            Vector2.up * Input.GetAxis("Player2Vertical");
    }

    private void FixedUpdate()
    {
        if (ControlledGoblin != null)
            ControlledGoblin.Rigid.velocity =
                _intendedMovement.normalized * ControlledGoblin.Speed * PlayerController.SpeedMultiplier;
    }
}