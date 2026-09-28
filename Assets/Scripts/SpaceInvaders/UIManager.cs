using System;
using TMPro;
using UnityEngine;

namespace SI
{
    public class UIManager : MonoBehaviour
    {
        [Header("GameUI")]
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _currentHealthText;

        [SerializeField] private TMP_Text _startGame;
        [SerializeField] private string _startGameStr = "PRESS KEY TO START";
        [SerializeField] private TMP_Text _gameOver;
        [SerializeField] private string _gameOverStr = "GAME IS OVER";

        private void Start()
        {
            _startGame.text = _startGameStr;
            _gameOver.text = _gameOverStr;
            _startGame.gameObject.SetActive(true);

            GameManager.StartGame += OnStartGame;
            GameManager.GameOver += OnGameOver;

            GameManager.ChangeScore += OnChangeScore;
            Player.HealthUpdate += OnPlayerHit;
        }

        private void OnDestroy()
        {
            GameManager.StartGame -= OnStartGame;
            GameManager.GameOver -= OnGameOver;

            GameManager.ChangeScore -= OnChangeScore;
            Player.HealthUpdate -= OnPlayerHit;
        }

        private void OnChangeScore(int score)
        {
            _currentScoreText.text = $"SCORE: {score}";
        }

        private void OnPlayerHit(int health)
        {
            _currentHealthText.text = $"HEALTH: {health}";
        }

        private void OnStartGame()
        {
            _startGame.gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            _gameOver.gameObject.SetActive(true);
        }
    }
}