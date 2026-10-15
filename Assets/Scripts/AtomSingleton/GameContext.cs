using UnityEngine;

namespace AtomSingleton
{
    public class GameContext : A_Singleton<GameContext>
    {
        [SerializeField] private int _coinsPerCollect = 1;
        public int CoinsPerCollect => _coinsPerCollect;
    }
}