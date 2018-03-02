using System.Threading.Tasks;
using UnityEngine;


public class Chain : MonoBehaviour
{
    private Gnox _gnox;
    private Krilk _krilk;

    private Rigidbody2D _gnoxRigid;
    private Rigidbody2D _krilkRigid;

    [SerializeField, Tooltip("Max distnce that can seperate Gnox and Krilk")] private int _maxDistance;

    private void Start()
    {
        _gnox = FindObjectOfType<Gnox>();
        _krilk = FindObjectOfType<Krilk>();

        _gnoxRigid = _gnox.GetComponent<Rigidbody2D>();
        _krilkRigid = _krilk.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        //make sure krilk and gnox are within range

        if (((_gnoxRigid.position + _gnoxRigid.velocity*Time.deltaTime) - (_krilkRigid.position + _krilkRigid.velocity*Time.deltaTime)).magnitude > _maxDistance)
        {
            //give gnox, krilks velocity and set krilk to zero
            _gnoxRigid.velocity = _krilkRigid.velocity;
            _krilkRigid.velocity = Vector2.zero;
        }
    }
}