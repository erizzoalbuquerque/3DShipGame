using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruction : MonoBehaviour
{
    [SerializeField] float _lifeTime = 5f;

    float _time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_time >= _lifeTime)
            Autodestruct();
        else
            _time += Time.deltaTime;
    }

    void Autodestruct()
    {
        Destroy(this.gameObject);
    }
}
