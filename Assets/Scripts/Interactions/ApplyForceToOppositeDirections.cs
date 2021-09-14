using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace YakisobaGang.Interactions
{
    public class ApplyForceToOppositeDirections : MonoBehaviour
    {
        [SerializeField, Tag] private string whoCanTriggerThis;
        [SerializeField] private UnityEvent whenCollisionHappens;
        [SerializeField] private float force = 10f;
        
        private Rigidbody _targetRb;
        private int _targetID;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag(whoCanTriggerThis))
                return;

            if (!other.gameObject.TryGetComponent<Rigidbody>(out var rb))
                return;


            if (_targetRb is null || _targetID != other.gameObject.GetInstanceID())
            {
                _targetRb = rb;
                _targetID = other.gameObject.GetInstanceID();
            }

            Vector3 dir = (transform.position - other.transform.position).normalized * -1;
            
            whenCollisionHappens?.Invoke();
            _targetRb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}