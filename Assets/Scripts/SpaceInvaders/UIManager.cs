using TMPro;
using UnityEngine;

namespace SI
{
    public class UIManager : MonoBehaviour
    {
        [Header("GameUI")]
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _currentHealthText;

        private void Start()
        {
            GameManager.ChangeScore += OnChangeScore;
            Player.HealthUpdate += OnPlayerHit;
        }

        private void OnDestroy()
        {
            
        }

        private void OnChangeScore(int score)
        {
            _currentScoreText.text = $"SCORE: {score}";
        }

        private void OnPlayerHit(int health)
        {
            _currentHealthText.text = $"HEALTH: {health}";
        }
    }
}