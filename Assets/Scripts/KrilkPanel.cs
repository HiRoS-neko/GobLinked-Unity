using TMPro;
using UnityEngine;


    public class KrilkPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _attack;
        [SerializeField] private TextMeshProUGUI _armor;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _speed;

        private void Update()
        {
            _attack.text = GlobalScript.Krilk.Attack.ToString();
            _armor.text = GlobalScript.Krilk.Armor.ToString();
            _health.text = GlobalScript.Krilk.Health.ToString();
            _speed.text = GlobalScript.Krilk.Speed.ToString();
        }
    }