using UnityEngine;
using UnityEngine.UI;

namespace StaticCalculator
{
    public class UI_Calculator : MonoBehaviour
    {
        [SerializeField] private Calculate _calculate = new();
        [SerializeField] private Button _calculateButton;

        private void Start()
        {
            _calculateButton.onClick.AddListener(OnCalculateClick);
        }

        private void OnDestroy()
        {
            _calculateButton.onClick.RemoveAllListeners();
        }

        private void OnCalculateClick()
        {

        }
    }
}