using TMPro;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI AttackStandard, AttackRange, AttackSupport, AttackUltimate;

    public void SetCooldownAttackStandard(int num)
    {
        if (num > 0)AttackStandard.text = num.ToString();
        else AttackStandard.text = "";
    }
    public void SetCooldownAttackRange(int num)
    {
        if (num > 0)AttackRange.text = num.ToString();
        else AttackRange.text = "";
    }
    public void SetCooldownAttackSupport(int num)
    {
        if (num > 0)AttackSupport.text = num.ToString();
        else AttackSupport.text = "";
    }
    public void SetCooldownAttackUltimate(int num)
    {
        if (num > 0)AttackUltimate.text = num.ToString();
        else AttackUltimate.text = "";
    }
}