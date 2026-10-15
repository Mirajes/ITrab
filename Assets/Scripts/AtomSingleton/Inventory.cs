using UnityEngine;

namespace AtomSingleton
{
    public class Inventory : A_Singleton<Inventory>
    {
        [SerializeField] private MoreCoinPerCollect _moreCoinPerCollectUpgrade;
        public MoreCoinPerCollect MoreCoinPerCollect => _moreCoinPerCollectUpgrade;
    }
}