using TMPro;
using UnityEngine;

public class GnoxPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armor;
    [SerializeField] private TextMeshProUGUI _attack;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _speed;

    private void Update()
    {
        _attack.text = GlobalScript.Gnox.GetAttack().ToString();
        _armor.text = GlobalScript.Gnox.GetArmor().ToString();
        _health.text = GlobalScript.Gnox.GetHealth().ToString();
        _speed.text = GlobalScript.Gnox.GetSpeed().ToString();
    }
}