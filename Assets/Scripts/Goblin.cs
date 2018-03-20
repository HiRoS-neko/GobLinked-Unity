using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class Goblin : MonoBehaviour
{
    public enum GoblinType
    {
        Krilk,
        Gnox,
        Both
    }

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

    public Inventory Items;

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

    public Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        CurrentHealth = Health;
    }


    private void Update()
    {
        var temp = Rigid.velocity;

        if (Mathf.Abs(temp.x) <= Mathf.Abs(temp.y) && Mathf.Abs(temp.y) > 0.1)
        {
            if (temp.y > 0) //up
                Anim.SetInteger("dir", 1);
            else if (temp.y < 0) //down
                Anim.SetInteger("dir", 3);
        }
        else if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y) && Mathf.Abs(temp.x) > 0.1)
        {
            if (temp.x > 0) //right
                Anim.SetInteger("dir", 2);
            else if (temp.x < 0) //left
                Anim.SetInteger("dir", 4);
        }
        else
        {
            Anim.SetInteger("dir", 0);
        }
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


    private void OnCollisionEnter2D(Collision2D other)
    {
        //check if its an item
        if (other.gameObject.CompareTag("Item"))
        {
            var temp = other.gameObject.GetComponent<Item>();
            Items.AddItem(temp);
            temp.transform.position = Vector3.zero;
            temp.gameObject.SetActive(false);
        }
        //or its an enemy
        else if (other.gameObject.CompareTag("Enemy"))
        {
            //TODO are we doing contact damage?
        }
    }

    public void useConsumable(Consumable item)
    {
        throw new NotImplementedException();
    }
}