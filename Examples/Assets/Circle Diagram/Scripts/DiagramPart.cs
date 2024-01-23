using UnityEngine;

namespace CircleDiagram
{
    public class DiagramPart : MonoBehaviour
    {
        [SerializeField] private LineRenderer _renderer;

        [SerializeField] private float _startDegrees = 0;
        [SerializeField] private float _endDegrees = 360f;

        [SerializeField] private Vector3[] _points;

        public void Init(Vector3[] points)
        {
            _points = points;
            DrawPart();
        }

        private void DrawPart()
        {
            _renderer.positionCount = _points.Length;
            _renderer.SetPositions(_points);
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject);
        }
    }
}