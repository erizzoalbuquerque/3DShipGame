using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] WaterBodyPhysics _waterBodyPhysics;
    [SerializeField] float _gravityMultiplier = 1f;
    [SerializeField] float _lifeTime = 10f;
    [SerializeField] GameObject _explosionPrefab = default;

    Rigidbody _rb;
    float _startTime;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _startTime > _lifeTime)
            Explode();

        if (_waterBodyPhysics.IsOnWater)
            Explode();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Physics.gravity * (_gravityMultiplier - 1f));
    }

    void Explode()
    {
        Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject);
    }

    public void Setup(Vector3 startVelocity)
    {
        _rb.velocity = startVelocity;
    }
}
