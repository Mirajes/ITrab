using UnityEngine;

namespace AtomSingleton
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private Initializer _initializer = new();

        private void Start()
        {
            _initializer.InitInputs(_player);
        }

        private void OnDestroy()
        {
            _initializer.DeinitInputs(_player);
        }

        private void ResetProgress() { }
    }
}