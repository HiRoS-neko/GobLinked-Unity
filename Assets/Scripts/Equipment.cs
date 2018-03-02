using UnityEngine;

public class Equipment : Item
{
    [Tooltip("Armor modifier to creature it is equipped to")]
    public int ArmorMod;

    [Tooltip("Attack modifier to creature it is equipped to")]
    public int AttackMod;

    [Tooltip("Health modifier to creature it is equipped to")]
    public int HealthMod;

    [Tooltip("Speed modifier to creature it is equipped to")]
    public int SpeedMod;
}