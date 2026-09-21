using TMPro;
using UnityEngine;

public class SI_UIManager : MonoBehaviour
{
    [Header("GameUI")]
    [SerializeField] private TMP_Text _currentScoreText;
    public void UpdateScoreText(int score)
    {
        _currentScoreText.text = $"SCORE: {score}";
    }
}