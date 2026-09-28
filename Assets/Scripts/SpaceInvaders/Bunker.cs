using System.Collections.Generic;
using UnityEngine;

namespace SI
{
    public class Bunker : MonoBehaviour
    {
        [SerializeField] private LayerMask _bulletMask;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private List<Sprite> _breakStages;
        [SerializeField] private int _damageCapacityPerStage = 4;
        private int _currentStage = 0;
        private int _remainingDamage;

        private void Start()
        {
            if (_breakStages.Count <= 0)
            {
                Debug.Log("[Bunker] - no stages");
                return;
            }

            _currentStage = 0;
            _remainingDamage = _damageCapacityPerStage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & _bulletMask) == 0)
                return;

            Destroy(collision.gameObject);
            _remainingDamage--;

            if (_remainingDamage <= 0)
                SwitchStage();
        }

        private void SwitchStage()
        {
            _currentStage++;

            if (_currentStage > _breakStages.Count - 1)
            {
                Destroy(this.gameObject);
                return;
            }

            _renderer.sprite = _breakStages[_currentStage];
            _remainingDamage = _damageCapacityPerStage;
        }
    }
}