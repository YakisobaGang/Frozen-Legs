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
            foreach (var joint in joints)
            {
                joint.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        [ContextMenu("EnableRagdoll")]
        public void EnableRagdoll()
        {
            foreach (var joint in joints)
            {
                joint.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}