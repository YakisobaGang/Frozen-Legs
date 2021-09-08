using UnityEngine;

namespace YakisobaGang.Interactable.Interactions
{
    public class EnableUIElement : InteractableBase
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private bool isToggle = false;

        public override void Interact()
        {
            if (isToggle)
                panel.SetActive(!panel.activeInHierarchy);
            else
                panel.SetActive(true);
        }
    }
}