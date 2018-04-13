using UnityEngine;

public class Krilk : Goblin
{
    [SerializeField] private GameObject _charge;
    [SerializeField] private GameObject _slam;
    [SerializeField] private GameObject _smash;


    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    public override void AttackStandard()
    {
        if (CooldownStandard > 0)
            return;

        CooldownStandard = 0.2f;

        Anim.SetTrigger("Slam");
        
        Instantiate(_slam, transform.position, Quaternion.identity);
        base.AttackStandard();
    }

    public override void AttackRange()
    {
        if (CooldownRange > 0)
            return;

        CooldownRange = 14 - 1 * RankRange;

        Anim.SetTrigger("Smash");
        
        Instantiate(_smash, transform.position, Quaternion.identity);
        base.AttackRange();
    }

    public override void AttackSupport()
    {
        if (CooldownSupport > 0)
            return;

        CooldownSupport = 30 - 2 * RankSupport;

        BlockHits = RankSupport;

        base.AttackSupport();
    }

    public override void AttackUltimate()
    {
        if (CooldownUltimate > 0)
            return;

        CooldownUltimate = 120 - 15 * RankUltimate;

        Anim.SetTrigger("Charge");
        
        Instantiate(_charge, transform.position, Quaternion.identity);
        base.AttackUltimate();
    }
}