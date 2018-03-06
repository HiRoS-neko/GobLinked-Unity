using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
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

    [HideInInspector] public HealthManager HealthUI;


    [HideInInspector] public Rigidbody2D Rigid;


    public int Speed => _baseSpeed + (EquippedAccessory != null ? EquippedAccessory.SpeedMod : 0) +
                        (EquippedWeapon != null ? EquippedWeapon.SpeedMod : 0);

    public int Health => _baseHealth + (EquippedAccessory != null ? EquippedAccessory.HealthMod : 0) +
                         (EquippedWeapon != null ? EquippedWeapon.HealthMod : 0);

    public int CurrentHealth;

    public int Armor => _baseArmor + (EquippedAccessory != null ? EquippedAccessory.ArmorMod : 0) +
                        (EquippedWeapon != null ? EquippedWeapon.ArmorMod : 0);

    public int Attack => _baseAttack + (EquippedAccessory != null ? EquippedAccessory.AttackMod : 0) +
                         (EquippedWeapon != null ? EquippedWeapon.AttackMod : 0);


    private int _lastHealth;
    private int _lastCurrentHealth;

    private void Start()
    {
        CurrentHealth = Health;
    }


    private void FixedUpdate()
    {
        //Make sure the health is set proportional when items are changed
        if (_lastHealth < Health)
        {
            CurrentHealth += Health - _lastHealth;
        }
        else if (_lastHealth > Health)
        {
            if (CurrentHealth < Health)
            {
                CurrentHealth = Health;
            }
        }

        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }

        if (_lastCurrentHealth != CurrentHealth)
        {
            HealthUI.SetHealth(CurrentHealth);
        }

        if (_lastHealth != Health)
        {
            HealthUI.SetMaxHealth(Health);
        }

        _lastHealth = Health;
        _lastCurrentHealth = CurrentHealth;
    }


    public void TakeDamage(int damage)
    {
        //TODO do armour calc and apply to health
        //HealthUI.SetHealth(Health);
    }
}