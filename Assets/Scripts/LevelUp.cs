using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private bool _p11 = false,
        _p12 = false,
        _p13 = false,
        _p14 = false,
        _p21 = false,
        _p22 = false,
        _p23 = false,
        _p24 = false;

    private void Update()
    {
        if ((GlobalScript.PlayerController.Player1.ControlledGoblin.RankRange - 1) +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankSupport - 1) +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankStandard - 1) +
            (GlobalScript.PlayerController.Player1.ControlledGoblin.RankUltimate - 1) < Goblin.Level)
        {
            if (Input.GetAxisRaw("Player1AttackStandard") > 0.5 && !_p11)
            {
                _p11 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankStandard++;
            }
            else
            {
                _p11 = false;
            }

            if (Input.GetAxisRaw("Player1AttackRange") > 0.5 && !_p12)
            {
                _p12 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankRange++;
            }
            else
            {
                _p12 = false;
            }

            if (Input.GetAxisRaw("Player1AttackSupport") > 0.5 && !_p13)
            {
                _p13 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankSupport++;
            }
            else
            {
                _p13 = false;
            }

            if (Input.GetAxisRaw("Player1AttackUltimate") > 0.5 && !_p14)
            {
                _p14 = true;
                GlobalScript.PlayerController.Player1.ControlledGoblin.RankUltimate++;
            }
            else
            {
                _p14 = false;
            }
        }

        if ((GlobalScript.PlayerController.Player2.ControlledGoblin.RankStandard - 1) +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankRange - 1) +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankSupport - 1) +
            (GlobalScript.PlayerController.Player2.ControlledGoblin.RankUltimate - 1) < Goblin.Level)
        {
            if (Input.GetAxisRaw("Player2AttackStandard") > 0.5 && !_p21)
            {
                _p21 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankStandard++;
            }
            else
            {
                _p21 = false;
            }

            if (Input.GetAxisRaw("Player2AttackRange") > 0.5 && !_p22)
            {
                _p22 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankRange++;
            }
            else
            {
                _p22 = false;
            }

            if (Input.GetAxisRaw("Player2AttackSupport") > 0.5 && !_p23)
            {
                _p23 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankSupport++;
            }
            else
            {
                _p23 = false;
            }

            if (Input.GetAxisRaw("Player2AttackUltimate") > 0.5 && !_p23)
            {
                _p24 = true;
                GlobalScript.PlayerController.Player2.ControlledGoblin.RankUltimate++;
            }
            else
            {
                _p24 = false;
            }
        }
    }
}