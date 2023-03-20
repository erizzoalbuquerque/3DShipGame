using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterBodyPhysics))]
public class Splash : MonoBehaviour
{
    [SerializeField] GameObject _splashPrefab;
    [SerializeField] float _minIntervalOutOfWaterToSplash = 1f;

    WaterBodyPhysics _waterBody;
    float _timeSinceOutOfWater;
    bool _wasBodyOnWaterLastFrame;

    private void Awake()
    {
        _waterBody = GetComponent<WaterBodyPhysics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeSinceOutOfWater = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_waterBody.IsOnWater == false)
            _timeSinceOutOfWater += Time.deltaTime;

        if (_wasBodyOnWaterLastFrame == false && _waterBody.IsOnWater == true)
        {
            if (_timeSinceOutOfWater > _minIntervalOutOfWaterToSplash)
                CreateSplash();

            _timeSinceOutOfWater = 0f;
        }

        _wasBodyOnWaterLastFrame = _waterBody.IsOnWater;
    }

    void CreateSplash()
    {
        Instantiate(_splashPrefab, this.transform.position,Quaternion.identity);
    }
}
