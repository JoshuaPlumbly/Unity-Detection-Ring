using Plumbly.Interactables;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Interactable))]
public class ThrowingItem : MonoBehaviour, IHandheldItem
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _throwForce;

    private bool _isHeld = false;
    private TrailRenderer _trailRenderer;
    private Interactable _intractable;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _intractable = GetComponent<Interactable>();

        _trailRenderer.emitting = false;
    }

    public void Use()
    {
        if (!_isHeld)
            return;

        Transform cameraTransform = Camera.current.transform;
        Vector3 forceToAdd = cameraTransform.forward * _throwForce;

        _rigidBody.transform.parent = null;
        _rigidBody.transform.position = cameraTransform.position + cameraTransform.forward;
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(forceToAdd, ForceMode.VelocityChange);
        _isHeld = false;

        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = true;
            StartCoroutine(WaitUntillStationery(() => _trailRenderer.emitting = false));
        }
    }

    public void Start()
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = false;
            _trailRenderer.Clear();
        }

        _rigidBody.isKinematic = true;
        _rigidBody.transform.localPosition = Vector3.zero;
        _isHeld = true;
    }

    private IEnumerator WaitUntillStationery(System.Action callback, float timeStilFor = 1f)
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime;

            if (_rigidBody.velocity.magnitude > 0.01f)
                t = 0f;
            else if (t <= timeStilFor)
                break;

            yield return null;
        }

        callback();
    }

    public GameObject Prefab()
    {
        return gameObject;
    }
}

