using UnityEngine;

public class ConsumableBuff : Consumable
{
    [Tooltip("Amount the buff affects the stat by")]
    public int Amount;

    [Tooltip("Type of buff to apply to the creature")]
    public Goblin.Stat Buff;

    [Tooltip("Length of the buff")] public int Duration;
}