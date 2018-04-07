using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Firebolt : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _velocity;
    private Rigidbody2D _rgd;

    private int _rank;

    private Vector3 _pos;
    private Vector2 _dir;
    private float _distance;

    /// <summary>
    /// Distance 1-2 (3) 3-4 (4) 5-6 (5) + AOE
    /// </summary>
    private void Start()
    {
        _rgd = GetComponent<Rigidbody2D>();
        //get level to run at
        _rank = GlobalScript.Gnox.RankStandard;
        _dir = GlobalScript.Gnox.Dir;

        _distance = Mathf.FloorToInt((_rank - 1) / 2) + 3;
        //get current position
        _pos = transform.position;
        
        var rot_z = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        
        
        _rgd.velocity = _velocity * transform.right;
    }

    private void FixedUpdate()
    {
        if ((transform.position - _pos).magnitude > _distance) //TODO add explosion when rank == 5 or 6
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //TODO
            //var temp = other.gameObject.GetComponent<>();
        }

        Destroy(gameObject);
    }
}