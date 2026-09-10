using UnityEngine;

public class MBh_LightsBlink : MonoBehaviour
{
    [Header("CORE")]
    [SerializeField] private Light _pointLight;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Material _material;

    [Header("Light Settings")]
    [SerializeField] private Color _lightColor = Color.yellow;
    [SerializeField] private float _maxLightIntensity = 10f;
    [SerializeField] private float _lightSpeed = 1f;

    private void Update()
    {
        BlinkLight();
    }

    private void OnEnable()
    {
        _pointLight.color = _lightColor;
    }

    private void BlinkLight()
    {
        _pointLight.intensity = _curve.Evaluate(Time.time * _lightSpeed) * _maxLightIntensity;
        _material.color = new Color(
            _curve.Evaluate(Time.time * _lightSpeed),
            _curve.Evaluate(Time.time * _lightSpeed),
            0
        );
    }
}