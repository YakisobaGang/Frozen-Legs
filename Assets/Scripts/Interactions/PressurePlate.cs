using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace YakisobaGang.Interactions
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField, Tag] private string whoCanTriggerThis;
        [SerializeField] private UnityEvent whenTimeHasPasse;
        [SerializeField] private float timeToTrigger = 1f;

        private WaitForSeconds _waitForSeconds;
        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(timeToTrigger);
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (!coll.CompareTag(whoCanTriggerThis))
                return;

            if (timeToTrigger > 0)
            {
                StartCoroutine(nameof(WaitTime));
                return;
            }
                
            whenTimeHasPasse?.Invoke();
        }

        private IEnumerator WaitTime()
        {
            yield return _waitForSeconds;
            
            whenTimeHasPasse?.Invoke();
        }
    }
}