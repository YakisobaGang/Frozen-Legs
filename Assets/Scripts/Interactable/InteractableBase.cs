using UnityEngine;

namespace YakisobaGang.Interactable
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public virtual void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}