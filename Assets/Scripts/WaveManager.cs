using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] bool _noWaves = false;

    [SerializeField] Material _waterMaterial;

    [SerializeField]
    float _waveAmplitude1 = 1f,
        _waveFrequency1 = 0.1f,
        _waveLength1 = 10f,
        _wavePhase1 = 0f;

    [SerializeField]
    float _waveAmplitude2 = 1f,
        _waveFrequency2 = 0.1f,
        _waveLength2 = 10f,
        _wavePhase2 = 0f;

    float _currentWaveAmplitude1,
        _currentWaveFrequency1,
        _currentWaveLength1,
        _currentwavePhase1;

    float _currentWaveAmplitude2,
        _currentWaveFrequency2,
        _currentWaveLength2,
        _currentwavePhase2;

    void Awake()
    {

    }

    private void Start()
    {
        UpdateWaveParameters(_waveAmplitude1, _waveFrequency1, _waveLength1, _wavePhase1,
                              _waveAmplitude2, _waveFrequency2, _waveLength2, _wavePhase2);
    }

    // Update is called once per frame
    void Update()
    {
        if (_noWaves == true)
            UpdateWaveParameters(0f, 1f, 1f, 0f, 0f, 1f, 1f, 0f);
        else
            UpdateWaveParameters(_waveAmplitude1, _waveFrequency1, _waveLength1, _wavePhase1,
                              _waveAmplitude2, _waveFrequency2, _waveLength2, _wavePhase2);

    }

    void UpdateWaveParameters(float waveAmplitude1, float waveFrequency1, float waveLength1, float wavePhase1,
                              float waveAmplitude2, float waveFrequency2, float waveLength2, float wavePhase2)
    {
        _waterMaterial.SetFloat("_WaveAmplitude1", waveAmplitude1);
        _waterMaterial.SetFloat("_WaveFrequency1", waveFrequency1);
        _waterMaterial.SetFloat("_WaveLength1", waveLength1);
        _waterMaterial.SetFloat("_WavePhase1", wavePhase1);

        _waterMaterial.SetFloat("_WaveAmplitude2", waveAmplitude2);
        _waterMaterial.SetFloat("_WaveFrequency2", waveFrequency2);
        _waterMaterial.SetFloat("_WaveLength2", waveLength2);
        _waterMaterial.SetFloat("_WavePhase2", wavePhase2);
    }

    public float GetWaterHeightAtPoint(Vector3 position)
    {
        if (_noWaves)
            return 0f;

        float waveHeight = _waveAmplitude1 * Mathf.Sin(2 * Mathf.PI * (_waveFrequency1 * Time.time + position.x / (2 * _waveLength1)) + _wavePhase1) +
            _waveAmplitude2 * Mathf.Sin(2 * Mathf.PI * (_waveFrequency2 * Time.time + position.z / (2 * _waveLength2)) + _wavePhase2);

        return waveHeight;
    }
}
