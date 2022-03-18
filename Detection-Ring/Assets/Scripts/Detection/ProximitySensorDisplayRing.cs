using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly.DetectionSystems
{
    [RequireComponent(typeof(LineRenderer))]
    public class ProximitySensorDisplayRing : MonoBehaviour
    {
        [SerializeField] private ProximitySensor2 _proximitySensor;
        [SerializeField] private int _segmentCount = 180;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _heightScale = 0.8f;
        [SerializeField] private AnimationCurve _strengthCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f), new Keyframe(2f, 1.5f), new Keyframe(5f, 2f));

        private LineRenderer _lineRenderer;
        private Vector3[] _ringPositions;
        private float[] _proximityData = new float[0];

        public const float tau = (float)Mathf.PI * 2f;

        private void Awake()
        {
            _ringPositions = PatternGenerator.Ring(_segmentCount, _radius);
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.loop = true;

            if (_proximitySensor == null)
                Debug.LogWarning(this + " is missing a proximity refrence.");
        }

        private void OnEnable()
        {
            _proximitySensor.UpdatePowerStatus += OnUpdatePowerStatus;
            _proximitySensor.UpdateProximityData += OnUpdateProximityData;
        }

        private void OnDisable()
        {
            _proximitySensor.UpdatePowerStatus -= OnUpdatePowerStatus;
            _proximitySensor.UpdateProximityData -= OnUpdateProximityData;

            _lineRenderer.enabled = false;
        }

        public void OnUpdatePowerStatus(bool isActive)
        {
            _lineRenderer.enabled = isActive;
        }

        private void OnUpdateProximityData(float[] proximityData)
        {
            _proximityData = proximityData;
        }

        private void LateUpdate()
        {
            _lineRenderer.positionCount = _proximityData.Length;

            if (_ringPositions == null || _ringPositions.Length != _proximityData.Length)
                _ringPositions = PatternGenerator.Ring(_proximityData.Length, _radius);

            for (int i = 0; i < _proximityData.Length; i++)
            {
                _ringPositions[i].y = EvaluateYPosition(_proximityData[i]);
                _lineRenderer.SetPosition(i, transform.position + _ringPositions[i]);
            }
        }

        private float EvaluateYPosition(float y)
        {
            return _strengthCurve.Evaluate(y) * _heightScale;
        }
    }
}