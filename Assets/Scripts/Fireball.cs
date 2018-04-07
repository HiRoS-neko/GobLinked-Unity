using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Fireball : MonoBehaviour
{
    private float _damage;
    private float _distance;
    private int _range;
    private int _rank;

    [SerializeField, Range(1, 5)] private float _velocity = 1;

    private Vector2 _dir;

    private Vector3 _pos;

    private Rigidbody2D _rgd;

    private void Start()
    {
        _rgd = GetComponent<Rigidbody2D>();

        _distance = 10;

        _rank = GlobalScript.Gnox.RankUltimate;
        _damage = (GlobalScript.Gnox.Attack + 1) * (1.1f * ((float) _rank / 5));
        _range = Mathf.FloorToInt(((float) _rank - 1) / 2) + 3;

        _dir = GlobalScript.Gnox.Dir;

        var rotZ = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


        _rgd.velocity = _velocity * transform.right;
    }


    private void FixedUpdate()
    {
        if ((transform.position - _pos).magnitude > _distance)
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        _rgd.velocity = Vector2.zero;

        var colliders = Physics2D.OverlapCircleAll(_rgd.position, _range).ToList();

        foreach (var coll in colliders)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                //TODO give the enemy some damage
            }
        }

        Destroy(this);
    }
}