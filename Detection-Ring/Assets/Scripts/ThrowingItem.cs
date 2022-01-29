using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowingItem : Tool
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _throwForce;

    private bool _isHeld = false;
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        
        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Launch();
    }

    private void Launch()
    {
        if (!_isHeld)
            return;

        _rigidBody.transform.parent = null;
        _rigidBody.isKinematic = false;
        _rigidBody.velocity = Camera.main.transform.forward * _throwForce;
        _isHeld = false;

        if (_trailRenderer != null)
        {
            _trailRenderer.emitting = true;
            StartCoroutine(RunWhenStill(() => _trailRenderer.emitting = false));
        }
    }

    public override void OnSelectedAsMain(Inventory inventory)
    {
        _rigidBody.isKinematic = true;
        _rigidBody.transform.parent = inventory.HandTransform;
        _rigidBody.transform.localPosition = Vector3.zero;
        _isHeld = true;
        _trailRenderer.emitting = false;
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

