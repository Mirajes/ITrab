using UnityEngine;

namespace AtomSingleton
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private UI_ProstoMagazin _prostoMagazin;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _prostoMagazin.Open();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _prostoMagazin.Close();
            }
        }
    }
}