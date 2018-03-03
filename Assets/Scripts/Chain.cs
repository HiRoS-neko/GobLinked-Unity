using System;
using TMPro;
using UnityEngine;


public class Chain : MonoBehaviour
{
    [SerializeField] private Gnox _gnox;
    [SerializeField] private Krilk _krilk;

    [SerializeField] private TextMeshProUGUI _debug;

    [SerializeField, Tooltip("Max distnce that can seperate Gnox and Krilk")]
    private int _maxDistance;

    void Update()
    {
        transform.position = (_krilk.transform.position + _gnox.transform.position) / 2;
        //make sure krilk and gnox are within range
        Debug.DrawLine(_gnox.Rigid.position, _krilk.Rigid.position, Color.red);

        
    }

    private void FixedUpdate()
    {
        var temp = Math.Abs(((_gnox.Rigid.position + _gnox.Rigid.velocity * Time.fixedDeltaTime) -
                             (_krilk.Rigid.position + _krilk.Rigid.velocity * Time.fixedDeltaTime)).magnitude);
        _debug.text = temp.ToString() + "\n" + _maxDistance.ToString();


        if (temp > _maxDistance)
        {
            Debug.Log("Out of Range");
            //give gnox, krilks velocity and set krilk to zero
            _gnox.Rigid.velocity = _krilk.Rigid.velocity;
            _krilk.Rigid.velocity = Vector2.zero;
        }
    }
}