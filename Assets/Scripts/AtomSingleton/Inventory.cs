using UnityEngine;

namespace AtomSingleton
{
    public class Inventory : A_Singleton<Inventory>
    {
        [SerializeField] private SO_Upgrade _moreCoinPerCollect;
        [SerializeField] private SO_Upgrade _passiveCoinEarning;
        [SerializeField] private SO_Upgrade _speedUp;
        [SerializeField] private SO_Upgrade _rotateSpeedUp;
        [SerializeField] private SO_Upgrade _raiseCollectRadius;
        [SerializeField] private SO_Upgrade _reduceCoinSpawnDelay;

        public SO_Upgrade MoreCoinPerCollect => _moreCoinPerCollect;
        public SO_Upgrade PassiveCoinEarning => _passiveCoinEarning;
        public SO_Upgrade SpeedUp => _speedUp;
        public SO_Upgrade RotateSpeedUp => _rotateSpeedUp;
        public SO_Upgrade RaiseCollectRadius => _raiseCollectRadius;
        public SO_Upgrade ReduceCoinSpawnDelay => _reduceCoinSpawnDelay;
    }
}