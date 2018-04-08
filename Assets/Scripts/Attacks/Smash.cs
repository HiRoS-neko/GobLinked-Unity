using UnityEngine;


public class Smash : MonoBehaviour
{

    private float _damage;
    private int _rank;

    private void Start()
    {
        _rank = GlobalScript.Krilk.RankRange;
        _damage = (GlobalScript.Krilk.Attack + 0.5f) * _rank;
    }
}