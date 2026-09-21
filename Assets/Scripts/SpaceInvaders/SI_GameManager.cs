using System;
using System.Collections;
using UnityEngine;

public class SI_GameManager : MonoBehaviour
{
    [Header("LINKS")]
    [SerializeField] private SI_UIManager _uiManager;
    [SerializeField] private SI_Player _player;

    [Header("CORE")]
    [SerializeField] private float _tickRate = 0.5f;
    private InputSystem_Actions _inputMap;
    private int _score;
    private bool _isPaused = false;
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
        }
    }

    public static event Action Step;
    public static event Action GameOver;
    public static Action<int> ChangeScore;

    private void Awake()
    {
        ChangeScore += OnChangedScore;
        GameOver += OnGameOver;

        InitInputs();
    }

    private void OnDestroy()
    {
        ChangeScore -= OnChangedScore;
        GameOver -= OnGameOver;

        DeInitInputs();
    }

    private IEnumerator StepRoutine()
    {
        WaitForSeconds wait = new(_tickRate);

        while (_player.IsAlive)
        {
            if (_isPaused)
            {
                yield return null;
                continue;
            }

            Step?.Invoke();

            yield return wait;
        }

        GameOver?.Invoke();
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
        if (_inputMap == null) return;

        _inputMap.Disable();
        _inputMap.PlayerSpace.Move.started -= _player.OnMoveInput;
        _inputMap.PlayerSpace.Shoot.started -= _player.OnShootInput;
        _inputMap.Dispose();
    }

    private void OnChangedScore(int score)
    {
        _uiManager.UpdateScoreText(score);
    }

    private void OnGameOver()
    {
        DeInitInputs();
    }
}
