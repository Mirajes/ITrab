using UnityEngine;

namespace AtomSingleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                if (Instance == null)
                {
                    Instance = this as T;
                    DontDestroyOnLoad(this.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);
                    return;
                }
            }
        }
    }
}