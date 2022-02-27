using UnityEngine;

public class InteractionSelector : MonoBehaviour, ISelector<Interactable>
{
    [SerializeField, Min(0)] float _reach = 1.5f;
    [SerializeField, Min(0)] float _casualReach = 1f;
    [SerializeField, Range(-1f, 1f)] float _threshold = 0.95f;

    private Interactable _selected;

    public Interactable GetDirectlyInsight(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Interactable interactable))
            {
                return interactable;
            }
        }

        return null;
    }

    public Ray CreateRay()
    {
        Transform camTransform = CameraManager.Current.transform;
        Vector3 cameraPosition = camTransform.position;
        Vector3 cameraDirection = Vector3.Normalize(camTransform.forward);
        return new Ray(cameraPosition, cameraDirection);
    }

    public void Check()
    {
        Transform camTransform = CameraManager.Current.transform;

        Vector3 position = transform.position;
        Ray ray = CreateRay();

        _selected = GetDirectlyInsight(ray);

        if (_selected != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(position, _reach);
        Smallest<Interactable> nearestAngle = new Smallest<Interactable>(null);
        Smallest<Interactable> nearestPosition = new Smallest<Interactable>(null);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Interactable interactable))
            {
                if (interactable.Ignore)
                    continue;

                Vector3 collidersPosition = colliders[i].transform.position;

                // Get interactable with the neastest to the center of screen.
                Vector3 toOrigin = Vector3.Normalize(collidersPosition - ray.origin);
                float angleToOrigin = Vector3.Dot(toOrigin, ray.direction);

                nearestAngle.Add(interactable, angleToOrigin);

                if (_selected != null)
                    continue;

                GetClosestToPosition(position, nearestPosition, interactable, collidersPosition);
            }
        }

    }

    private void GetClosestToPosition(Vector3 position, Smallest<Interactable> nearestPosition, Interactable interactable, Vector3 collidersPosition)
    {
        float distanceToPlayer = Vector3.Distance(collidersPosition, position);

        if (distanceToPlayer <= _casualReach)
            nearestPosition.Add(interactable, distanceToPlayer);
    }

    public bool WithinReach(Interactable interactable)
    {
        return Vector3.Distance(interactable.transform.position, transform.position) < _reach;
    }

    Interactable ISelector<Interactable>.GetSelection()
    {
        return _selected;
    }
}