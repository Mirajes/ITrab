using System;
using UnityEngine;
using UnityEngine.UI;

namespace AtomSingleton
{
    public class UI_PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;

        public static event Action Exit;

        private void Start()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
            _exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveListener(OnExitClick);
            _exitButton.onClick.RemoveListener(OnExitClick);
        }

        private void OnResumeClick()
        {
            this.gameObject.SetActive(false);
        }

        private void OnExitClick()
        {
            Exit?.Invoke();
        }
    }
}