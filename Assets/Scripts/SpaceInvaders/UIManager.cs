using TMPro;
using UnityEngine;

namespace SI
{
    public class UIManager : MonoBehaviour
    {
        [Header("GameUI")]
        [SerializeField] private TMP_Text _currentScoreText;
        public void UpdateScoreText(int score)
        {
            _currentScoreText.text = $"SCORE: {score}";
        }
    }
}