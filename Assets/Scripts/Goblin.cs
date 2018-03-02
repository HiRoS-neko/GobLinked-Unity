using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D Rigid;
    
    [SerializeField] [Tooltip("Base Armor of the Goblin")]
    private int _baseArmor;

    [SerializeField] [Tooltip("Base Attack of the Goblin")]
    private int _baseAttack;

    [SerializeField] [Tooltip("Base Health of the Goblin")]
    private int _baseHealth;

    [SerializeField] [Tooltip("Base Speed of the Goblin")]
    private int _baseSpeed;

    public Accessory EquippedAccessory;

    public Weapon EquippedWeapon;

    public List<Item> Inventory;


    public int Speed
    {
        get
        {
            return _baseSpeed + (EquippedAccessory != null ? EquippedAccessory.SpeedMod : 0) +
                   (EquippedWeapon != null ? EquippedWeapon.SpeedMod : 0);
        }
    }

    public int Health
    {
        get
        {
            return _baseHealth + (EquippedAccessory != null ? EquippedAccessory.HealthMod : 0) +
                   (EquippedWeapon != null ? EquippedWeapon.HealthMod : 0);
        }
    }

    public int Armor
    {
        get
        {
            return _baseArmor + (EquippedAccessory != null ? EquippedAccessory.ArmorMod : 0) +
                   (EquippedWeapon != null ? EquippedWeapon.ArmorMod : 0);
        }
    }

    public int Attack
    {
        get
        {
            return _baseAttack + (EquippedAccessory != null ? EquippedAccessory.AttackMod : 0) +
                   (EquippedWeapon != null ? EquippedWeapon.AttackMod : 0);
        }
    }
}