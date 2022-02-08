using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Intractable))]
public class ThrowingItem : Tool
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _throwForce;

    private bool _isHeld = false;
    private TrailRenderer _trailRenderer;
    private Intractable _intractable;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _intractable = GetComponent<Intractable>();

        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Launch();
    }

    public void Launch()
    {
        if (!_isHeld)
            return;

        _intractable.Ignore = false;

        Transform cameraTransform = CameraManager.Current.transform;
        Vector3 forceToAdd = cameraTransform.forward * _throwForce;

        _rigidBody.transform.parent = null;
        _rigidBody.transform.position = cameraTransform.position + cameraTransform.forward;
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(forceToAdd, ForceMode.VelocityChange);
        _isHeld = false;

        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = true;
            StartCoroutine(RunWhenStill(() => _trailRenderer.emitting = false));
        }
    }

    public override void OnSelectedAsMain(Inventory inventory)
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = false;
            _trailRenderer.Clear();
        }

        _intractable.Ignore = true;

        _rigidBody.isKinematic = true;
        _rigidBody.transform.parent = inventory.HandTransform;
        _rigidBody.transform.localPosition = Vector3.zero;
        _isHeld = true;
    }

    private IEnumerator RunWhenStill(System.Action callback, float timeStilFor = 1f)
    {
        bool isStill = false;
        float t = 0f;

        while (true)
        {
            isStill = _rigidBody.velocity.magnitude < 0.01f;

            t += Time.deltaTime;

            if (!isStill)
                t = 0f;
            else if (t <= timeStilFor)
                break;

            yield return null;
        }

        callback();
    }
}

