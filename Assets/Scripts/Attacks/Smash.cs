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

        Collider2D[] results = new Collider2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        int num;
        
        switch (_rank)
        {
            case 1:
            case 2:
                _firstRank.enabled = true;
                num = Physics2D.OverlapCollider(_firstRank, contactFilter, results);
                break;
            case 3:
            case 4:
                _secondRank.enabled = true;
                num = Physics2D.OverlapCollider(_secondRank, contactFilter, results);
                break;
            default:
                _thirdRankPlus.enabled = true;
                num = Physics2D.OverlapCollider(_thirdRankPlus, contactFilter, results);
                break;
        }

        for (int i = 0; i < num; i++)
        {
            if (results[i].CompareTag("Enemy"))
            {
                var enemy = results[i].GetComponent<EnemyPathfinding>();
                enemy.TakeDamage((int)_damage);
            }
        }

        _firstRank.enabled = false;
        _secondRank.enabled = false;
        _thirdRankPlus.enabled = false;
        
        Destroy(gameObject, 0.2f);
        
    }
}