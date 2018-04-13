using UnityEngine;

public class Slam : MonoBehaviour
{
    private float _damage;

    private int _rank;
    [SerializeField] private Collider2D _firstRank, _secondRank, _thirdRankPlus;

    private void Start()
    {
        _rank = GlobalScript.Krilk.RankStandard;
        _damage = (GlobalScript.Krilk.Attack) * (1.2f) + ((float) _rank / 10);

        Collider2D[] results = new Collider2D[10];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        int num;

        var dir = GlobalScript.Krilk.Dir;
        var rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

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
                enemy.TakeDamage((int) _damage);
            }
        }


        Destroy(gameObject, 0.2f);
    }
}