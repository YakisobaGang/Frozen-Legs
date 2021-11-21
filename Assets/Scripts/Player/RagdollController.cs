using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YakisobaGang.Player
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private List<CharacterJoint> joints = new List<CharacterJoint>();
        [SerializeField] private bool disableAtStart = false;
        [HideInInspector] public bool ragDollIsDisable = false;

        private void Start()
        {
            if (disableAtStart)
                DisableRagdoll();
            else
                EnableRagdoll();
        }

        [ContextMenu("Disable Ragdoll")]
        public void DisableRagdoll()
        {
            ragDollIsDisable = true;

            foreach (var joint in joints)
            {
                joint.GetComponent<Collider>().isTrigger = true;
            }
        }

        [ContextMenu("Enable Ragdoll")]
        public void EnableRagdoll()
        {
            ragDollIsDisable = false;

            foreach (var joint in joints)
            {
                joint.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}