using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    // Use this for initialization

    [SerializeField] private GameObject _lookTarget;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = _lookTarget.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}