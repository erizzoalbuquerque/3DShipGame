using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float _lifeTime = 10f;

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
    }

    void Explode()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void Setup(Vector3 startVelocity)
    {
        _rb.velocity = startVelocity;
    }
}
