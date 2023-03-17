using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSpeed : MonoBehaviour
{
    [SerializeField] float _limitSpeed = 15f;
    [SerializeField] float _limitAngularSpeed = 5f;

    Rigidbody _rb;

    void Awake()
    {
        _rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.velocity.magnitude > _limitSpeed)
            _rb.velocity = _rb.velocity.normalized * _limitSpeed;

        if (_rb.angularVelocity.magnitude > _limitAngularSpeed)
            _rb.angularVelocity = _rb.angularVelocity.normalized * _limitAngularSpeed;

    }
}
