using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    [SerializeField] private Collider2D _rank1, _rank2, _rank3;

    private float _damage;

    private int _rank;

    // Use this for initialization
    void Start()
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
            case 5:
            case 6:
                _rank3.enabled = true;
                break;
        }
    }

    // Update is called once per frame
}