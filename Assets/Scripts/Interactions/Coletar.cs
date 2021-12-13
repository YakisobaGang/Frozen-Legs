using UnityEngine;
using System;

namespace YakisobaGang.Interactions
{
    public class Coletar : MonoBehaviour
    {
        private bool jafoi = true;
        
        public static event Action <int> OnPickup; 
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Pegou!");
                OnPickup?.Invoke(1);
                Destroy(gameObject);
            }
        }
    }
}
