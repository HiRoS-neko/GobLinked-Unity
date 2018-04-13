﻿using System;
using System.Collections;
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

    public enum Stat
    {
        Speed,
        Health,
        Armor,
        Attack
    }

    public static int Exp = 12000;

    [SerializeField] [Tooltip("Base Armor of the Goblin")]
    private int _baseArmor;

    [SerializeField] [Tooltip("Base Attack of the Goblin")]
    private int _baseAttack;

    [SerializeField] [Tooltip("Base Health of the Goblin")]
    private int _baseHealth;

    [SerializeField] [Tooltip("Base Speed of the Goblin")]
    private int _baseSpeed;

    private int _lastCurrentHealth;

    private int _lastHealth;

    [HideInInspector] public AbilityManager AbilityManager;

    [HideInInspector] public Animator Anim;

    [HideInInspector] public int BlockHits;


    //Cooldowns
    public float CooldownStandard, CooldownRange, CooldownSupport, CooldownUltimate;

    public int CurrentHealth;


    public Vector2 Dir; // direction used for attacks

    public Accessory EquippedAccessory;

    public Weapon EquippedWeapon;

    [HideInInspector] public HealthManager HealthUI;

    public Inventory Items;


    //Rank in Abilities
    public int RankStandard = 1, RankRange = 1, RankSupport = 1, RankUltimate = 1;
    public Vector2 RightStick; // direction used for attacks

    [HideInInspector] public Rigidbody2D Rigid;

    public static int Level
    {
        get
        {
            var temp = Exp;
            var level = 0;
            do
            {
                level++;
                temp -= (int) (10 * (level - Mathf.Pow(level, 0.25f)));
            } while (temp > 0);

            level--;

            return level;
        }
    }

    public int Speed => _baseSpeed + (EquippedAccessory != null ? EquippedAccessory.SpeedMod : 0) +
                        (EquippedWeapon != null ? EquippedWeapon.SpeedMod : 0);

    public int Health => _baseHealth + (EquippedAccessory != null ? EquippedAccessory.HealthMod : 0) +
                         (EquippedWeapon != null ? EquippedWeapon.HealthMod : 0);

    public int Armor => _baseArmor + (BlockHits > 0 ? 50 : 0) +
                        (EquippedAccessory != null ? EquippedAccessory.ArmorMod : 0) +
                        (EquippedWeapon != null ? EquippedWeapon.ArmorMod : 0);

    public int Attack => _baseAttack + (EquippedAccessory != null ? EquippedAccessory.AttackMod : 0) +
                         (EquippedWeapon != null ? EquippedWeapon.AttackMod : 0);

    private void Start()
    {
        Anim = GetComponent<Animator>();
        CurrentHealth = Health;
        RankStandard = 1;
        RankRange = 1;
        RankSupport = 1;
        RankUltimate = 1;
    }


    private void Update()
    {
        var temp = Rigid.velocity;

        if (temp.magnitude > 0.1)
            Dir = temp.normalized; //last movement direction,,, used for attacks

        if (RightStick.magnitude > 0.1f)
            Dir = RightStick.normalized;

        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (Vector3) Dir);

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

        //if (Input.GetKey("z"))
        //    Exp += 1;


        //Cooldowns
        if (CooldownStandard > 0) CooldownStandard -= Time.deltaTime;
        if (CooldownRange > 0) CooldownRange -= Time.deltaTime;
        if (CooldownSupport > 0) CooldownSupport -= Time.deltaTime;
        if (CooldownUltimate > 0) CooldownUltimate -= Time.deltaTime;

        AbilityManager.SetCooldownAttackRange((int) CooldownRange);
        AbilityManager.SetCooldownAttackStandard((int) CooldownStandard);
        AbilityManager.SetCooldownAttackSupport((int) CooldownSupport);
        AbilityManager.SetCooldownAttackUltimate((int) CooldownUltimate);

        AbilityManager.SetRankStandard(RankStandard);
        AbilityManager.SetRankRange(RankRange);
        AbilityManager.SetRankSupport(RankSupport);
        AbilityManager.SetRankUltimate(RankUltimate);
    }

    private void FixedUpdate()
    {
        //Make sure the health is set proportional when items are changed
        if (_lastHealth < Health)
            CurrentHealth += Health - _lastHealth;
        else if (_lastHealth > Health)
            if (CurrentHealth < Health)
                CurrentHealth = Health;

        if (CurrentHealth > Health) CurrentHealth = Health;

        if (_lastCurrentHealth != CurrentHealth) HealthUI.SetHealth(CurrentHealth);

        if (_lastHealth != Health) HealthUI.SetMaxHealth(Health);

        _lastHealth = Health;
        _lastCurrentHealth = CurrentHealth;
    }


    public void TakeDamage(int damage)
    {
        if (GlobalScript.invincible == false)
        {
            CurrentHealth -= Math.Max((damage - Armor),1);
        }
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

    public void UseConsumable(Consumable item)
    {
        if (item.GetType() == typeof(ConsumableRestore)) throw new NotImplementedException();

        if (item.GetType() == typeof(ConsumableBuff))
        {
            var consumable = (ConsumableBuff) item;
            StartCoroutine(AddStat(consumable.Duration, consumable.Buff, consumable.Amount));
        }
    }


    public IEnumerator AddStat(float duration, Stat statType, int amount)
    {
        //add amount to stat
        throw new NotImplementedException();
        yield return new WaitForSeconds(duration);
        //remove amount to stat
    }


    public virtual void AttackStandard()
    {
    }

    public virtual void AttackRange()
    {
    }

    public virtual void AttackSupport()
    {
    }

    public virtual void AttackUltimate()
    {
    }
}