using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    // Use this for initialization

    [SerializeField] private GameObject _lookTarget;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var diff = _lookTarget.transform.position - transform.position;
        diff.Normalize();

        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}