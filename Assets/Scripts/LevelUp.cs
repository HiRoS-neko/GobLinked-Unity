﻿using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private bool _p11,
        _p12,
        _p13,
        _p14,
        _p21,
        _p22,
        _p23,
        _p24;

    private void Update()
    {
        if (GlobalScript.PlayerController.Player1.ControlledGoblin.RankRange - 1 +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankSupport - 1) +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankStandard - 1) +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankUltimate - 1) < Goblin.Level)
        {
            if (Input.GetAxisRaw("Player1AttackStandard") > 0.5 && !_p11)
            {
                _p11 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankStandard++;
            }
            else if (Input.GetAxisRaw("Player1AttackStandard") < 0.5)
            {
                _p11 = false;
            }

            if (Input.GetAxisRaw("Player1AttackRange") > 0.5 && !_p12)
            {
                _p12 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankRange++;
            }
            else if (Input.GetAxisRaw("Player1AttackRange") < 0.5)
            {
                _p12 = false;
            }

            if (Input.GetAxisRaw("Player1AttackSupport") > 0.5 && !_p13)
            {
                _p13 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankSupport++;
            }
            else if (Input.GetAxisRaw("Player1AttackSupport") < 0.5)
            {
                _p13 = false;
            }

            if (Input.GetAxisRaw("Player1AttackUltimate") > 0.5 && !_p14)
            {
                _p14 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankUltimate++;
            }
            else if (Input.GetAxisRaw("Player1AttackUltimate") < 0.5)
            {
                _p14 = false;
            }
        }

        if (GlobalScript.PlayerController._gameMode == PlayerController.GameMode.MultiPlayer &&
            GlobalScript.PlayerController.Player2.ControlledGoblin.RankRange - 1 +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankSupport - 1) +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankStandard - 1) +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankUltimate - 1) < Goblin.Level)
        {
            if (Input.GetAxisRaw("Player2AttackStandard") > 0.5 && !_p21)
            {
                _p21 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankStandard++;
            }
            else if (Input.GetAxisRaw("Player2AttackStandard") < 0.5)
            {
                _p21 = false;
            }

            if (Input.GetAxisRaw("Player2AttackRange") > 0.5 && !_p22)
            {
                _p22 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankRange++;
            }
            else if (Input.GetAxisRaw("Player2AttackRange") < 0.5)
            {
                _p22 = false;
            }

            if (Input.GetAxisRaw("Player2AttackSupport") > 0.5 && !_p23)
            {
                _p23 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankSupport++;
            }
            else if (Input.GetAxisRaw("Player2AttackSupport") < 0.5)
            {
                _p23 = false;
            }

            if (Input.GetAxisRaw("Player2AttackUltimate") > 0.5 && !_p24)
            {
                _p24 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankUltimate++;
            }
            else if (Input.GetAxisRaw("Player2AttackUltimate") < 0.5)
            {
                _p24 = false;
            }
        }
    }
}