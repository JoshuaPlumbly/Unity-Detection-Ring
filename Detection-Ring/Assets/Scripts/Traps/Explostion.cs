using UnityEngine;

[System.Serializable]
public class Explostion
{
    [SerializeField] protected int _damage = 100;
    [SerializeField] protected float _force = 700f;
    [SerializeField] protected float _range = 9f;

    public void Explode(Vector3 position)
    {
        var colliders = Physics.OverlapSphere(position, _range);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_force, position, _range);
            }

            if (colliders[i].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                var hitPoint = colliders[i].ClosestPoint(position);
                var hitDirection = colliders[i].transform.position - position;
                damageable.TakeDamage(_damage, hitPoint, hitDirection);
            }
        }
    }
}