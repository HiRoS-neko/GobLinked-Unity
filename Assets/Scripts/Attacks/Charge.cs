using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Charge : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _speed;

    private float _damage;
    private int _rank;
    private float _distance = 5;

    private Rigidbody2D _rgd;
    private CircleCollider2D _coll;

    private Vector2 _direction;
    private Vector2 _pos;

    private void Start()
    {
        _coll = GetComponent<CircleCollider2D>();

        _rgd = GetComponent<Rigidbody2D>();
        _rank = GlobalScript.Krilk.RankUltimate;

        _coll.radius = Mathf.Sqrt(_rank) * 0.5f;
        _damage = (GlobalScript.Krilk.Attack + 2) * _rank;

        _direction = GlobalScript.Krilk.Dir == Vector2.zero ? Vector2.right : GlobalScript.Krilk.Dir;
        _pos = _rgd.position;
        _rgd.velocity = _speed * _direction;
    }

    private void Update()
    {
        GlobalScript.Krilk.Rigid.MovePosition(_rgd.position);
        _rgd.position = GlobalScript.Krilk.Rigid.position;
        if ((_rgd.position - _pos).magnitude > _distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = GetComponent<EnemyPathfinding>();
            enemy.TakeDamage((int) _damage);
        }
    }
}