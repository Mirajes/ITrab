using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SI
{
    public class GameManager : MonoBehaviour
    {
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                ChangeScore?.Invoke(Score);
            }
        }

        [Header("LINKS")]
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Player _player;
        [SerializeField] private SideWall _sideWall_R;
        [SerializeField] private SideWall _sideWall_L;

        [Header("CORE")]
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private float _tickRate = 0.3f;
        [SerializeField] private float _tickPerDie = 0.005f;
        [SerializeField] private float _tickRateCap = 0.1f;

        private InputSystem_Actions _inputMap;
        private int _score;
        private bool _isPaused = false;
        private IEnumerator _stepRoutine;


        public static event Action Step;
        public static event Action StartGame;
        public static event Action GameOver;
        public static event Action<int> PlayerWin;
        public static event Action<int> ChangeScore;

        private void Awake()
        {
            MoveSideWalls();
            InitInputs();

            EnemyManager.AllEnemiesDie += OnAllEnemiesDie;
            GameOver += OnGameOver;
            Enemy.Die += OnEnemyDie;
            EndLine.EnemyTouch += OnTouchGameOver;
        }


        private void OnDestroy()
        {
            DeInitInputs();

            EnemyManager.AllEnemiesDie -= OnAllEnemiesDie;
            GameOver -= OnGameOver;
            Enemy.Die -= OnEnemyDie;
            EndLine.EnemyTouch -= OnTouchGameOver;
        }

        private void OnStartGame(InputAction.CallbackContext context)
        {
            _inputMap.PlayerSpace.Move.started -= OnStartGame;
            _inputMap.PlayerSpace.Shoot.started -= OnStartGame;

            _stepRoutine = StepRoutine();
            StartCoroutine(_stepRoutine);

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

            _inputMap.GameSpace.Pause.started += OnPauseInput;
            _inputMap.GameSpace.Restart.started += OnRestartInput;

            InitPlayerInputs();

            _inputMap.Enable();
        }

        private void InitPlayerInputs()
        {
            _inputMap.PlayerSpace.Move.started += OnStartGame;
            _inputMap.PlayerSpace.Shoot.started += OnStartGame;


            _inputMap.PlayerSpace.Move.started += _player.OnMoveInput;
            _inputMap.PlayerSpace.Shoot.started += _player.OnShootInput;
        }

        private void DeInitInputs()
        {
            if (_inputMap == null) return;

            _inputMap.Disable();

            _inputMap.GameSpace.Pause.started -= OnPauseInput;
            _inputMap.GameSpace.Restart.started -= OnRestartInput;

            _inputMap.Dispose();
        }

        private void DeInitPlayerInputs()
        {
            _inputMap.PlayerSpace.Move.started -= OnStartGame;
            _inputMap.PlayerSpace.Shoot.started -= OnStartGame;
            _inputMap.PlayerSpace.Move.started -= _player.OnMoveInput;
            _inputMap.PlayerSpace.Shoot.started -= _player.OnShootInput;
        }

        private void OnEnemyDie(Enemy enemy)
        {
            OnEnemyDieScore(enemy.ScoreToGive);
            ChangeTickRate();
        }

        private void OnAllEnemiesDie()
        {
            StopCoroutine(_stepRoutine);
            PlayerWin?.Invoke(_score);
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

        private void OnTouchGameOver() // vot eto kasha
        {
            StopCoroutine(_stepRoutine);
            GameOver?.Invoke();
        }

        private void OnGameOver()
        {
            DeInitPlayerInputs();
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

        private void ChangeTickRate()
        {
            if (_tickRate < _tickRateCap)
            {
                _tickRate = _tickRateCap;
                return;
            }

            _tickRate -= _tickPerDie;
        }
    }
}