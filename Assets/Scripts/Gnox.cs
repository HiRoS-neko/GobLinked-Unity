using System;
using UnityEngine;

public class Gnox : Goblin
{
    [SerializeField] private GameObject _backwave;
    [SerializeField] private GameObject _fireball;
    [SerializeField] private GameObject _firebolt;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    public override void AttackStandard()
    {
        // Fire Bolt
        // 
        // Cooldown 0.2 Seconds
        // A*(1.0+(X/10))
        // Small Explosion at Level 6
        // 
        // 1-2: 3 squares
        // 3-4: 4 squares
        // 5-6: 5 squares
        // 

        if (CooldownStandard > 0)
            return;


        Anim.SetTrigger("Firebolt");
        CooldownStandard = 0.2f;

        Instantiate(_firebolt, transform.position, Quaternion.identity);
        base.AttackStandard();
    }

    public override void AttackRange()
    {
        // Backwave
        // 
        // Cooldown 10-(0.5*X) Seconds
        // A*(0.25+(X/20))
        // Knocks enemies away 3+(int)(X/2)
        // 
        // AOE
        // 
        // 1-3: 3 squares
        // 4-6: 5 squares
        // 

        if (CooldownRange > 0)
            return;
        Anim.SetTrigger("Shockwave");

        CooldownRange = 10 - 0.5f * RankRange;
        Instantiate(_backwave, transform.position, Quaternion.identity);

        //Physics2D.OverlapCircleAll()
        base.AttackRange();
    }

    public override void AttackSupport()
    {
        // Revivify
        // 
        // Cooldown 20 - (1*X) Seconds
        // Restores 1*X Health
        // 
        // If Krilk is dead, bring him back
        // 

        if (CooldownSupport > 0)
            return;

        Anim.SetTrigger("Heal");
        CooldownSupport = 20 - 1 * RankSupport;

        GlobalScript.Gnox.CurrentHealth += RankSupport;

        GlobalScript.Krilk.CurrentHealth += RankSupport;


        base.AttackSupport();
    }

    public override void AttackUltimate()
    {
        // Fireball
        // 
        // Cooldown 120 - 10 * X Seconds
        // (P+1) * (1.1*(X/5))
        // 
        // AOE
        // 
        // 1-2 3 squares
        // 3-4 4 squares
        // 5-6 5 squares
        // 

        if (CooldownUltimate > 0)
            return;
        Anim.SetTrigger("Fireball");

        CooldownUltimate = 120 - 10 * RankUltimate;

        Instantiate(_fireball, transform.position, Quaternion.identity);

        base.AttackUltimate();
    }



}