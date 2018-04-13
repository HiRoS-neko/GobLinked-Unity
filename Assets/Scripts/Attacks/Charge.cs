using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Charge : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _speed;

    private float _damage;
    private int _rank;
    private float _distance = 5;

    private Rigidbody2D _rgd;

    private Vector2 _direction;
    private Vector2 _pos;

    private void Start()
    {
        _rgd = GetComponent<Rigidbody2D>();
        _rank = GlobalScript.Krilk.RankUltimate;
        _damage = (GlobalScript.Krilk.Attack + 2) * _rank;

        _direction = GlobalScript.Krilk.Dir;
        _pos = _rgd.position;
        _rgd.velocity = _speed * _direction;
    }

    private void Update()
    {
        if ((_rgd.position - _pos).magnitude > _distance)
        {
            
        }
    }
}