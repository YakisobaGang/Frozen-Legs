using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEditor;

namespace YakisobaGang.Interactions
{
    public class MovingBlock : MonoBehaviour
    {
        [SerializeField] private List<Transform> wayPoints = new List<Transform>();

        [Header("Animation Settings"), Space]
        [SerializeField] private float duration = 5f;
        [SerializeField] private float delay = 4;
        [SerializeField] private Ease ease;

        [Header("Gizmos"), Space]
        [SerializeField] private float size = 0.3f;

        private Transform _myTransform;
        private int _index = 0;

        private void Awake()
        {
            _myTransform = GetComponent<Transform>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (wayPoints.Count == 0 || wayPoints.Any((p) => p is null))
                return;

            wayPoints.ForEach((point) =>
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(point.position, size);
            });
            
            Handles.color = Color.green;
            Handles.DrawAAPolyLine(wayPoints.Select((p) => p.position).ToArray());
            Handles.DrawAAPolyLine(new Vector3[] {wayPoints.Last().position, wayPoints.First().position});
        }
#endif

        private void Update()
        {
            if (_index >= wayPoints.Count)
                _index = 0;

            if ((int)Vector3.Distance(_myTransform.position, wayPoints[_index].position) > 0)
            {
                _myTransform.DOMove(wayPoints[_index].position, duration)
                    .SetDelay(delay)
                    .SetEase(ease);
            }
            else
                _index++;

        }
    }
}