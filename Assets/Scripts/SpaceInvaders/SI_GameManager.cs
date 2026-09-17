using System;
using UnityEngine;

public class SI_GameManager : MonoBehaviour
{
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
        }
    }

    private int _score;
    [SerializeField] private SI_Player _player;
    [SerializeField] private SI_UIManager _uiManager;

    private InputSystem_Actions _inputMap;

    public static Action<int> ChangeScore;

    private void Awake()
    {
        ChangeScore += OnChangedScore;

        InitInputs();
    }

    private void OnDestroy()
    {
        ChangeScore -= OnChangedScore;

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

    private void OnChangedScore(int score)
    {
        _uiManager.UpdateScoreText(score);
    }
}