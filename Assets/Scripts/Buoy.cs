using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Buoy : MonoBehaviour
{
    [SerializeField] float _buoyancy = 10f;
    [SerializeField] List<BuoyPart> buoyParts;

    float _waveAmplitude1, _waveFrequency1, _waveLength1, _wavePhase1;
    float _waveAmplitude2, _waveFrequency2, _waveLength2, _wavePhase2;
    Rigidbody _rb;
    float _submergence;

    public float WaveAmplitude1 { get => _waveAmplitude1; set => _waveAmplitude1 = value; }
    public float WaveFrequency1 { get => _waveFrequency1; set => _waveFrequency1 = value; }
    public float WaveLength1 { get => _waveLength1; set => _waveLength1 = value; }
    public float WavePhase1 { get => _wavePhase1; set => _wavePhase1 = value; }
    public float WaveAmplitude2 { get => _waveAmplitude2; set => _waveAmplitude2 = value; }
    public float WaveFrequency2 { get => _waveFrequency2; set => _waveFrequency2 = value; }
    public float WaveLength2 { get => _waveLength2; set => _waveLength2 = value; }
    public float WavePhase2 { get => _wavePhase2; set => _wavePhase2 = value; }
    public float Submergence { get => _submergence; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        ApplyBuoyForce();                
    }


    void ApplyBuoyForce()
    {
        float totalVolume = 0f;
        foreach (BuoyPart bp in buoyParts)
        {
            totalVolume += Mathf.Pow(bp.Radius, 3);
        }

        float submergence = 0;
        foreach (BuoyPart bp in buoyParts)
        {
            Vector3 centerPosition = GetBuoyPartCenterPosition(bp);

            float waterSurfaceHeight = GetWaterHeight(centerPosition);

            float volumePercentage = (Mathf.Pow(bp.Radius, 3) / totalVolume);

            float rateVolumeDisplaced = bp.GetRateVolumeDisplaced(centerPosition.y, waterSurfaceHeight);

            float buoyForce = _rb.mass *  -Physics.gravity.y * _buoyancy * rateVolumeDisplaced * volumePercentage;
            _rb.AddForceAtPosition(buoyForce * Vector3.up, centerPosition);

            submergence += rateVolumeDisplaced * volumePercentage;

            //print(bp.GetRateVolumeDisplaced(centerPosition.y, waterSurfaceHeight));
            //print(buoyForce);
        }

        _submergence = submergence;
    }


    Vector3 GetBuoyPartCenterPosition(BuoyPart buoyPart)
    {
        return transform.TransformPoint(buoyPart.Offset);
    }


    float GetWaterHeight(Vector3 position)
    {
        float waveHeight = _waveAmplitude1 * Mathf.Sin(2 * Mathf.PI * (_waveFrequency1 * Time.time + position.x / (2 * _waveLength1)) + _wavePhase1) +
            _waveAmplitude2 * Mathf.Sin(2 * Mathf.PI * (_waveFrequency2 * Time.time + position.z / (2 * _waveLength2)) + _wavePhase2); 
        
        return  waveHeight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (BuoyPart bp in buoyParts)
        {
            Gizmos.DrawWireSphere(GetBuoyPartCenterPosition(bp), bp.Radius);
        }
    }

    [System.Serializable]
    class BuoyPart
    {
        public float Radius { get => _radius; set => _radius = value; }
        public Vector3 Offset { get => _offset; set => _offset = value; }

        [SerializeField] float _radius = 1f;
        [SerializeField] Vector3 _offset = Vector3.zero;

        public float GetRateVolumeDisplaced(float centerHeight, float waterSurfaceHeight)
        {
            float rateVolumeDisplaced;

            //Calculate distance from water surface to the lowest point on the sphere.
            float submergedHeight = waterSurfaceHeight - (centerHeight - _radius);

            //float volumeSphere = 4f / 3f * Mathf.PI * Mathf.Pow(_radius, 3);

            if (submergedHeight <= 0) // Above Water
            {
                rateVolumeDisplaced = 0f;
            }
            else if (submergedHeight >= 2 * _radius) // Completely submerged
            {
                rateVolumeDisplaced = 1f;
            }
            else // 0 <= submergedHeight <= 2R
            {
                rateVolumeDisplaced = Mathf.Sin(submergedHeight * Mathf.PI / (4 * _radius));
            }

            return rateVolumeDisplaced;
        }
    }
}
