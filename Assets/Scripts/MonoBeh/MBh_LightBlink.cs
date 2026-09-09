using UnityEngine;

public class MBh_LightsBlink : MonoBehaviour
{
    [Header("CORE")]
    [SerializeField] private Light _pointLight;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Material _fresnelOutlineMaterial;

    [Header("Light Settings")]
    [SerializeField] private Color _lightColor = Color.yellow;
    [SerializeField] private float _maxLightIntensity = 10f;
    [SerializeField] private float _lightSpeed = 1f;

    private void Update()
    {
        BlinkLight();
    }

    private void BlinkLight()
    {
        _pointLight.intensity = _curve.Evaluate(Time.time * _lightSpeed) * _maxLightIntensity;
    }
}
