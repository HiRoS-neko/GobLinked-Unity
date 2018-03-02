using UnityEngine;

public class ConsumableBuff : Consumable
{
    public enum BuffType
    {
        Armor,
        Attack,
        Speed,
        Health
    }

    [Tooltip("Amount the buff affects the stat by")]
    public int Amount;

    [Tooltip("Type of buff to apply to the creature")]
    public BuffType Buff;

    [Tooltip("Length of the buff")] public int Duration;
}