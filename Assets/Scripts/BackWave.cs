﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BackWave : MonoBehaviour
{
    [SerializeField, Range(1, 5)] private float _speed = 1;

    private float _range;
    private float _knock;
    private float _damage;

    private Vector3 _pos;

    private float _curr;


    private List<Collider2D> _objects;

    private void Start()
    {
        _pos = transform.position;
        _range = GlobalScript.Gnox.RankRange < 4 ? 3 : 5;
        _knock = 3 + Mathf.Floor(GlobalScript.Gnox.RankRange / 2);
        //get sphere of rigidbodys around the player
        _objects = Physics2D.OverlapCircleAll(GlobalScript.Gnox.Rigid.position, _range).ToList();

        _damage = GlobalScript.Gnox.Attack * (0.25f + (GlobalScript.Gnox.RankRange));
    }

    private void Update()
    {
        var ratio = _curr / _range;
        transform.localScale = Vector3.one * ratio * _range;
    }

    private void FixedUpdate()
    {
        var remove = new List<Collider2D>();
        foreach (var obj in _objects)
        {
            if ((obj.transform.position - _pos).magnitude < _range &&
                (obj.transform.position - _pos).magnitude < _curr && obj.CompareTag("Enemy"))
            {
                remove.Add(obj);
                obj.GetComponent<Rigidbody2D>()
                    .MovePosition(obj.transform.position + (_knock * (obj.transform.position - _pos).normalized));
                //TODO apply damage
            }
        }

        foreach (var rem in remove)
        {
            _objects.Remove(rem);
        }

        if (_curr > _range)
        {
            Destroy(this.gameObject);
        }

        _curr += Time.fixedDeltaTime * _speed;
    }
}