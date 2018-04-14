using TMPro;
using UnityEngine;

public class KrilkPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armor;
    [SerializeField] private TextMeshProUGUI _attack;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _speed;

    private void Update()
    {
        _attack.text = GlobalScript.Krilk.GetAttack().ToString();
        _armor.text = GlobalScript.Krilk.GetArmor().ToString();
        _health.text = GlobalScript.Krilk.GetHealth().ToString();
        _speed.text = GlobalScript.Krilk.GetSpeed().ToString();
    }
}