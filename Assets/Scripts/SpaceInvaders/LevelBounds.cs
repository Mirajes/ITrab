using UnityEngine;

[CreateAssetMenu(fileName = "LevelBounds", menuName = "Game/LevelBounds")]
public class LevelBounds : ScriptableObject
{
    [SerializeField] private float _width = 12f;
    [SerializeField] private float _height = 30f;

    public float Width => _width;
    public float Height => _height;

    public float MinX => -_width / 2f;
    public float MaxX => _width / 2f;
    public float MinY => -_height / 2f;
    public float MaxY => _height / 2f;
}