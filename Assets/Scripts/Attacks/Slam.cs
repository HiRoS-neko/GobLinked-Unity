using UnityEngine;

public class Slam : MonoBehaviour
{
    private float _damage;

    private int _rank;
    [SerializeField] private Collider2D _rank1, _rank2, _rank3Plus;

    private void Start()
    {
        _rank = GlobalScript.Krilk.RankStandard;

        switch (_rank)
        {
            case 1:
            case 2:
                _rank1.enabled = true;
                break;
            case 3:
            case 4:
                _rank2.enabled = true;
                break;
            default:
                _rank3Plus.enabled = true;
                break;
        }
    }
}