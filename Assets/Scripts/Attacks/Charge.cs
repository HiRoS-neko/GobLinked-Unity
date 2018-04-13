using UnityEngine;

public class Charge : MonoBehaviour
{
    private float _damage;
    private int _rank;

    private void Start()
    {
        _rank = GlobalScript.Krilk.RankUltimate;
        _damage = (GlobalScript.Krilk.Attack + 2) * _rank;
    }
}