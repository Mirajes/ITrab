using UnityEngine;

public class SI_GameManager : MonoBehaviour
{
    [SerializeField] private SI_Player _player;
    //private float _score;

    private InputSystem_Actions _inputMap;

    private void Awake()
    {
        InitInputs();
    }

    private void OnDestroy()
    {
        DeInitInputs();
    }

    private void InitInputs()
    {
        _inputMap = new();
        _inputMap.PlayerSpace.Move.started += _player.OnMoveInput;
        _inputMap.PlayerSpace.Shoot.started += _player.OnShootInput;
        _inputMap.Enable();
    }

    private void DeInitInputs()
    {
        _inputMap.Disable();
        _inputMap.PlayerSpace.Move.started -= _player.OnMoveInput;
        _inputMap.PlayerSpace.Shoot.started -= _player.OnShootInput;
        _inputMap.Dispose();
    }
}
