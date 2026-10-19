using System.Collections.Generic;
using UnityEngine;

namespace AtomSingleton
{
    [CreateAssetMenu(menuName = "Atom/Upgrade")]
    public class SO_Upgrade : ScriptableObject
    {
        [SerializeField] private string _id;
        public string ID => _id;

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _currentLevel;

        [SerializeField] private List<int> _levels = new();

        public bool IsMaxLevel => _currentLevel >= _levels.Count;

        public string Name => _name;
        public string Description => _description;
        public int CurrentLevel => _currentLevel;
        public int CurrentCost
        {
            get
            {
                if (IsMaxLevel)
                    return -1;

                return _levels[CurrentLevel];
            }
        }

        public void LevelUp()
        {
            if (!IsMaxLevel)
                _currentLevel += 1;
        }

        public void Remove()
        {
            _currentLevel -= 1;

            if (_currentLevel < 0)
            {
                _currentLevel = 0;
                Debug.Log($"[{this.name}] - I cant be less than 0");
            }
        }

        public void OnLoad(int savedLevel)
        {
            _currentLevel = savedLevel;
        }

        public void Reset()
        {
            _currentLevel = 0;
        }
    }
}