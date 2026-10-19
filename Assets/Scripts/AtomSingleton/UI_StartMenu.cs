using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtomSingleton
{
    public class UI_StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public static event Action Enter;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }
        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
        }

        private void OnStartClick()
        {
            Enter?.Invoke();
        }
    }
}