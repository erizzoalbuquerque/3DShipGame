using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBodyPhysics))]
public class ShipController : MonoBehaviour
{
    [SerializeField] float _maxForwardSpeed = 5f;
    [SerializeField] float _maxForwardAcceleration = 5f;

    [SerializeField] float _maxSteeringSpeed = 2f;
    [SerializeField] float _maxSteeringAcceleration = 4f;

    [SerializeField] GameObject _shotPrefab;
    [SerializeField] float _shotDistanceOffset = 1f;
    [SerializeField] float _shotStartSpeed = 5f;

    WaterBodyPhysics _waterBody;
    Rigidbody _rb;

    float _forwardInput, _steeringInput;

    float _desiredForwardSpeed;
    float _desiredSteeringSpeed;

    void Awake()
    {
        _waterBody = GetComponent<WaterBodyPhysics>();
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
        ////Forward
        //float forwardSpeed;
        //float currentForwardSpeed = this.transform.InverseTransformDirection(_rb.velocity).z;
        //
        //float maxSpeedChange = _maxForwardAcceleration * Time.deltaTime;
        //
        //float incrementalForwardSpeed;
        //if (Mathf.Abs(currentForwardSpeed) < _maxForwardSpeed)
        //    incrementalForwardSpeed = maxSpeedChange * _forwardInput;
        //else
        //    incrementalForwardSpeed = 0;
        //
        //forwardSpeed = currentForwardSpeed + incrementalForwardSpeed -
        //    currentForwardSpeed * _forwardDamping * Time.deltaTime;
        //
        //float sideSpeed;
        //float currentSideSpeed = this.transform.InverseTransformDirection(_rb.velocity).x;
        //
        //sideSpeed = currentSideSpeed - currentSideSpeed * _sideDamping * Time.deltaTime;
        //
        //_rb.velocity = this.transform.TransformDirection(new Vector3(sideSpeed, 0f, forwardSpeed));
        //
        //
        ////Steering
        //float steeringSpeed;
        //float currentSteeringSpeed = _rb.angularVelocity.y;
        //_desiredSteeringSpeed = _steeringInput * _maxSteeringSpeed;
        //
        //float maxSteeringSpeedChange = _maxSteeringAcceleration * Time.deltaTime * Mathf.Clamp01(forwardSpeed / _maxForwardSpeed);
        //
        //steeringSpeed = Mathf.MoveTowards(currentSteeringSpeed, _desiredSteeringSpeed, maxSteeringSpeedChange) -
        //    _angularDamping * currentSteeringSpeed * Time.deltaTime;
        //
        //_rb.angularVelocity = new Vector3(0f, steeringSpeed, 0f);
    }

    public void Shoot(bool shootRight)
    {
        //Vector3 shootDirection;
        //if (shootRight)
        //    shootDirection = Vector3.right;
        //else
        //    shootDirection = Vector3.left;
        //
        ////Dash
        //_rb.velocity = _rb.velocity + this.transform.TransformDirection(-shootDirection) * 10f;
        //
        ////Shot
        //GameObject shot = Instantiate(_shotPrefab, this.transform.position + _shotDistanceOffset * this.transform.TransformDirection(shootDirection), Quaternion.identity);
        //shot.GetComponent<Shot>().StartVelocity = this.transform.TransformDirection(shootDirection) * _shotStartSpeed;
    }
}
