using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace YakisobaGang.Interactions
{
    public class StandingOnTimer : MonoBehaviour
    {
        [SerializeField] private float time = 4f;
        [SerializeField] private bool resetOnExit = false;
        [HideInInspector] public float startTime;
        public UnityEvent<float> onTimeTick;
        public UnityEvent onTimeRest;
        public UnityEvent onTimeCompleted;


        private void Awake()
        {
            startTime = time;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var position = transform.position;
            Handles.Label(new Vector3(position.x, 1, position.z), $"Timer: {time:N2}");
        }
#endif

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            time -= Time.deltaTime;
            onTimeTick?.Invoke(time / startTime);

            if (time <= 0f)
            {
                onTimeCompleted?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (resetOnExit)
            {
                time = startTime;
                onTimeRest?.Invoke();
            }
        }
    }
}