using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SI
{
    public class GameManager : MonoBehaviour
    {
        private const float TICKRATE = 0.2f;

        [Header("LINKS")]
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Player _player;
        [SerializeField] private SideWall _sideWall_R;
        [SerializeField] private SideWall _sideWall_L;

        [Header("CORE")]
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private float _tickRate = 0.1f;
        [SerializeField] private float _tickPerDie = 0.01f;

        private InputSystem_Actions _inputMap;
        private int _score;
        private bool _isPaused = false;
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                ChangeScore?.Invoke(Score);
            }
        }

        public static event Action Step;
        public static event Action StartGame;
        public static event Action GameOver;
        public static Action<int> ChangeScore;

        private void Awake()
        {
            MoveSideWalls();
            InitInputs();

            GameOver += OnGameOver;
            Enemy.DieScore += OnEnemyDieScore;
            Enemy.Die += OnEnemyDie;
        }


        private void OnDestroy()
        {
            DeInitInputs();

            GameOver -= OnGameOver;
            Enemy.DieScore -= OnEnemyDieScore;
            Enemy.Die -= OnEnemyDie;
        }

        private void OnStartGame(InputAction.CallbackContext context)
        {
            _inputMap.PlayerSpace.Move.started -= OnStartGame;
            _inputMap.PlayerSpace.Shoot.started -= OnStartGame;

            StartCoroutine(StepRoutine());

            StartGame?.Invoke();
        }

        private IEnumerator StepRoutine()
        {
            //WaitForSeconds wait = new(_tickRate);

            while (_player.IsAlive)
            {
                if (_isPaused)
                {
                    yield return null;
                    continue;
                }

                Step?.Invoke();

                //yield return wait;
                yield return new WaitForSeconds(_tickRate);
            }

            GameOver?.Invoke();
        }

        private void InitInputs()
        {
            _inputMap = new();

            _inputMap.PlayerSpace.Move.started += OnStartGame;
            _inputMap.PlayerSpace.Shoot.started += OnStartGame;
            _inputMap.GameSpace.Pause.started += OnPauseInput;
            _inputMap.GameSpace.Restart.started += OnRestartInput;

            _inputMap.PlayerSpace.Move.started += _player.OnMoveInput;
            _inputMap.PlayerSpace.Shoot.started += _player.OnShootInput;
            _inputMap.Enable();
        }

        private void DeInitInputs()
        {
            if (_inputMap == null) return;

            _inputMap.Disable();

            _inputMap.PlayerSpace.Move.started -= OnStartGame;
            _inputMap.PlayerSpace.Shoot.started -= OnStartGame;
            _inputMap.GameSpace.Pause.started -= OnPauseInput;
            _inputMap.GameSpace.Restart.started -= OnRestartInput;

            _inputMap.PlayerSpace.Move.started -= _player.OnMoveInput;
            _inputMap.PlayerSpace.Shoot.started -= _player.OnShootInput;
            _inputMap.Dispose();
        }

        private void OnPauseInput(InputAction.CallbackContext context)
        {
            _isPaused = !_isPaused;

            if (_isPaused)
                _inputMap.PlayerSpace.Disable();
            else
                _inputMap.PlayerSpace.Enable();

        }

        private void OnRestartInput(InputAction.CallbackContext context)
        {
            SceneManager.LoadScene("SpaceInvaders");
        }

        private void OnGameOver()
        {
            DeInitInputs();
        }

        private void OnEnemyDieScore(int scoreToAdd)
        {
            Score += scoreToAdd;
        }

        private void MoveSideWalls()
        {
            _sideWall_L.transform.position = new Vector3(_levelBounds.MinX - _sideWall_L.WallSize, 0f);
            _sideWall_R.transform.position = new Vector3(_levelBounds.MaxX + _sideWall_R.WallSize, 0f);
        }

        private void OnEnemyDie()
        {
            _tickRate -= _tickPerDie;
        }
    }
}