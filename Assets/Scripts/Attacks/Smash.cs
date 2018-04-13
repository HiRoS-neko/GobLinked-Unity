using UnityEngine;

public class Smash : MonoBehaviour
{
    private float _damage;
    [SerializeField] private Collider2D _firstRank, _secondRank, _thirdRankPlus;
    private int _rank;

    private void Start()
    {
        _rank = GlobalScript.Krilk.RankRange;
        _damage = (GlobalScript.Krilk.Attack + 0.5f) * _rank;

        _firstRank.enabled = false;
        _secondRank.enabled = false;
        _thirdRankPlus.enabled = false;

        switch (_rank)
        {
            case 1:
            case 2:
                _firstRank.enabled = true;
                break;
            case 3:
            case 4:
                _secondRank.enabled = true;
                break;
            default:
                _thirdRankPlus.enabled = true;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) other.GetComponent<EnemyPathfinding>().TakeDamage((int) _damage);
    }
}