using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBodyPhysics))]
public class ShipController : MonoBehaviour
{
    [SerializeField]  Cannon _cannon;

    [SerializeField] float _maxForwardSpeed = 5f;
    [SerializeField] float _maxForwardAcceleration = 5f;

    [SerializeField] float _maxSteeringSpeed = 2f;
    [SerializeField] float _maxSteeringAcceleration = 4f;

    [SerializeField] float _jumpVelocity = 2f;

    WaterBodyPhysics _waterBody;
    Rigidbody _rb;

    float _forwardInput, _steeringInput;

    float _desiredForwardSpeed;
    float _desiredSteeringSpeed;

    void Awake()
    {
        _waterBody = GetComponent<WaterBodyPhysics>();
    }

    private void Start()
    {
        _rb = _waterBody.RigidBody;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetInput(float forwardInput, float steeringInput)
    {
        _forwardInput = Mathf.Clamp(forwardInput, -1f, 1f);
        _steeringInput = Mathf.Clamp(steeringInput, -1f, 1f);
    }

    void Move()
    {
        //Forward
        float forwardSpeed;
        Vector3 localVelocity = this.transform.InverseTransformDirection(_rb.velocity);
        float currentForwardSpeed = localVelocity.z;
        
        float maxSpeedChange = _maxForwardAcceleration * Time.deltaTime;
        
        float incrementalForwardSpeed;
        if (Mathf.Abs(currentForwardSpeed) < _maxForwardSpeed && _waterBody.IsOnWater)
            incrementalForwardSpeed = maxSpeedChange * _forwardInput;
        else
            incrementalForwardSpeed = 0;

        forwardSpeed = currentForwardSpeed + incrementalForwardSpeed;        
        
        _rb.velocity = this.transform.TransformDirection(new Vector3(localVelocity.x,localVelocity.y, forwardSpeed));
        
        
        //Steering
        float steeringSpeed;
        Vector3 localAngularVelocity = this.transform.InverseTransformDirection(_rb.angularVelocity);
        float currentSteeringSpeed = localAngularVelocity.y;
        _desiredSteeringSpeed = _steeringInput * _maxSteeringSpeed;
        
        float maxSteeringSpeedChange = _maxSteeringAcceleration * Time.deltaTime * Mathf.Clamp01(forwardSpeed / _maxForwardSpeed);

        steeringSpeed = Mathf.MoveTowards(currentSteeringSpeed, _desiredSteeringSpeed, maxSteeringSpeedChange);
        
        _rb.angularVelocity = this.transform.TransformDirection(new Vector3(localAngularVelocity.x, steeringSpeed, localAngularVelocity.z));
    }

    public void Shoot()
    {
        if (_cannon != null)
        {
            _cannon.Shoot(_rb.velocity);
        }
    }

    public void Jump()
    {
        if (_waterBody.IsOnWater)
            _rb.velocity += Vector3.up * _jumpVelocity;
    }
}
