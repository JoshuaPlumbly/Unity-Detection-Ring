using UnityEngine;

public class InteractionSelector : MonoBehaviour, ISelector<Interactable>
{
    [SerializeField, Min(0f)] float _reach = 1.8f;
    [SerializeField, Min(0f)] float _casualReach = 1.4f;
    [SerializeField, Range(0f, 180f)] float _threshold = 25f;

    private Interactable _selected;

    public Ray CreateRay()
    {
        Transform camTransform = CameraManager.Current.transform;
        Vector3 cameraPosition = camTransform.position;
        Vector3 cameraDirection = camTransform.forward;
        return new Ray(cameraPosition, cameraDirection);
    }

    private bool SelectFromRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Interactable interactable))
            {
                if (!WithinReach(interactable))
                    return false;

                _selected = interactable;
                return true;
            }
        }

        return false;
    }

    private void SelectNextBestPossibleInteractable(Ray ray)
    {
        Vector3 position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, _reach);
        Smallest<Interactable> smallestAngle = new Smallest<Interactable>(null);
        Smallest<Interactable> smallestDistance = new Smallest<Interactable>(null);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Interactable interactable))
            {
                Vector3 interactablePosition = colliders[i].transform.position;
                Vector3 vectorToInteractable = interactablePosition - ray.origin;
                float distanceToInteractable = Mathf.Abs(vectorToInteractable.magnitude);
                float angleFromDirection = Vector3.Angle(vectorToInteractable, ray.origin);

                CompareAngle(angleFromDirection, distanceToInteractable, interactable, smallestAngle);

                if (smallestAngle.GetItem() != null)
                    continue;
                
                CompareDistance(distanceToInteractable, interactable, smallestDistance);
            }
        }

        _selected = smallestAngle.GetItem();

        if (_selected == null)
            _selected = smallestDistance.GetItem();
    }

    private void CompareAngle(float angle, float distance, Interactable interactable, Smallest<Interactable> smallestAngle)
    {
        if (angle <= _threshold && distance <= _reach)
            smallestAngle.Add(interactable, angle);
    }

    private void CompareDistance(float distance, Interactable interactable, Smallest<Interactable> smallestDistance)
    {
        if (distance <= _casualReach)
            smallestDistance.Add(interactable, distance);
    }

    public bool WithinReach(Interactable interactable)
    {
        if (interactable == null)
            return false;

        return Vector3.Distance(interactable.transform.position, transform.position) < _reach;
    }

    public void Check()
    {
        _selected = null;
        Ray ray = CreateRay();

        SelectFromRay(ray);
        
        if (_selected == null)
            SelectNextBestPossibleInteractable(ray);
    }

    Interactable ISelector<Interactable>.GetSelection()
    {
        return _selected;
    }
}