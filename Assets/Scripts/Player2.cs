﻿using UnityEngine;


public class Player2 : MonoBehaviour
{
    [HideInInspector]
    public Goblin ControlledGoblin;

    private Vector2 _intendedMovement;

    private void Update()
    {
        //get player 1 input and control gobo
        
        _intendedMovement = Vector2.right*Input.GetAxis("Player2Horizontal") + Vector2.up*Input.GetAxis("Player2Vertical");
    }

    private void FixedUpdate()
    {
        ControlledGoblin.Rigid.velocity = _intendedMovement*ControlledGoblin.Speed;
    }
}