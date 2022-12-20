using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody)),RequireComponent(typeof(Buoy))]
public class WaterBodyPhysics : MonoBehaviour
{
    [SerializeField] Vector3 _waterDamping;
    [SerializeField] Vector3 _airDamping;

    [SerializeField] float _waterAngularDamping = 2f;
    [SerializeField] float _airAngularDamping = 1f;

    [SerializeField] float _submergenceTreshold = 0.1f;

    Rigidbody _rb;
    Buoy _buoy;

    [SerializeField] bool _isOnWater;

    Vector3 _currentDamping;
    float _currentAngularDamping;

    public Rigidbody RigidBody { get => _rb; }
    public Buoy Buoy { get => _buoy; }
    public bool IsOnWater { get => _isOnWater; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _buoy = GetComponent<Buoy>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _isOnWater = _buoy.Submergence >= _submergenceTreshold;
        if (_isOnWater)
        {
            _currentDamping = _waterDamping;
            _currentAngularDamping = _waterAngularDamping; 
        }
        else
        {
            _currentDamping = _airDamping;
            _currentAngularDamping = _airAngularDamping;
        }

        Vector3 localVeclocity = this.transform.InverseTransformDirection(_rb.velocity);
        Vector3 localDampingForce = new Vector3(-localVeclocity.x * _currentDamping.x, -localVeclocity.y * _currentDamping.y, -localVeclocity.z * _currentDamping.z);

        _rb.AddRelativeForce(localDampingForce);

        Vector3 localAngularVelocity = this.transform.InverseTransformDirection(_rb.angularVelocity);
        Vector3 localDampingTorque = -_currentAngularDamping * localAngularVelocity;

        _rb.AddRelativeTorque(localDampingTorque);
    }
}
