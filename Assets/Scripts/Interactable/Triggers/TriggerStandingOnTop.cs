using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace YakisobaGang.Interactable.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerStandingOnTop : MonoBehaviour
    {
        [SerializeField, InfoBoxAttribute("O collider tem que ser trigger", EInfoBoxType.Warning),Space] 
        private UnityEvent onInteract;
        [SerializeField, Tag] private string coliderTag;
        [SerializeField] public IInteractable test;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(coliderTag))
                onInteract?.Invoke();
        }
    }
}