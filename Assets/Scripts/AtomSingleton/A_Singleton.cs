using UnityEngine;

namespace AtomSingleton
{
    public abstract class A_Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}