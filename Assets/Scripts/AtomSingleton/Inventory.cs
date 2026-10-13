using UnityEngine;

namespace AtomSingleton
{
    public class Inventory : Singleton<Inventory>
    {
        [SerializeField] private MoreCoinsPerCollect _moreCoinsPerCollectUpgrade;
        public MoreCoinsPerCollect MoreCoinsPerCollect => _moreCoinsPerCollectUpgrade;
    }
}