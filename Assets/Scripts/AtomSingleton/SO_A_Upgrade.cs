using UnityEngine;

namespace AtomSingleton
{
    public abstract class SO_A_Upgrade : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _cost_Base;

        [SerializeField] private int _count;
        [SerializeField] private int _cost_Current;
        [SerializeField] private float _raisePerBuy;

        public string Name => _name;
        public int Count => _count;
        public int CurrentCost => _cost_Current;

        public virtual void Add(int count)
        {
            _count += count;
        }

        public virtual void Remove(int count)
        {
            _count -= count;

            if (_count < 0)
            {
                _count = 0;
                Debug.Log($"[{this.name}] - I cant be less than 0");
            }
        }

        public virtual void Reset()
        {
            _count = 0;
            _cost_Current = _cost_Base;
        }
    }
}