using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] Vector3 _startVelocity = Vector3.forward;
    [SerializeField] float _maxTravelDistance = 10f;

    Rigidbody _rb;
    Vector3 _lastPostion;
    float _currentTravelDistance;

    public Vector3 StartVelocity { get => _startVelocity; set => _startVelocity = value; }

    // Start is called before the first frame update
    void Start()
    {
        _currentTravelDistance = 0;
        _lastPostion = this.transform.position;

        _rb = this.GetComponent<Rigidbody>();
        _rb.velocity = _startVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTravelDistance();

        if (_currentTravelDistance >= _maxTravelDistance)
            Destroy(this.gameObject);
    }

    void CalculateTravelDistance()
    {
        float delta = (this.transform.position - _lastPostion).magnitude;
        _currentTravelDistance += delta;

        _lastPostion = this.transform.position;
    }

    void Explode()
    {

    }
}
